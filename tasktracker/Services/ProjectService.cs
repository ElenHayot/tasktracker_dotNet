using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Mappers;
using tasktracker.Repositories;

namespace tasktracker.Services
{
    /// <summary>
    /// Project service - manage Projects calls
    /// </summary>
    public class ProjectService : IProjectService
    {
        #region Instancies
        /// <summary>
        /// Local project repository instance
        /// </summary>
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Local task repository instance
        /// </summary>
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Local logger instance for ProjectService
        /// </summary>
        private readonly ILogger<ProjectService> _logger;
        #endregion

        /// <summary>
        /// ProjectService constructor
        /// </summary>
        /// <param name="projectRepository">Project repository instance</param>
        /// <param name="taskRepository">Task repository instance</param>
        /// <param name="logger">Project service logger instance</param>
        public ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _logger = logger;
        }

        #region Public methods
        /// <inheritdoc/>
        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto project)
        {

            ProjectEntity entity = ProjectMapper.ToCreateEntity(project);
            ProjectEntity createdEntity;
            try
            {
                createdEntity = await _projectRepository.CreateProjectAsync(entity);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqlEx && sqlEx.SqliteErrorCode == 19)
            {
                throw new TitleAlreadyExistsException($"There's already a project named '{entity.Title}'. Please modify your title.");
            }
            ProjectDto projectDto = ProjectMapper.ToDto(createdEntity);
            return projectDto;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProjectAsync(int id, bool forceTaskDeleting = false)
        {
            ProjectEntity? projectEntity = await _projectRepository.GetProjectByIdAsync(id);
            if (projectEntity == null)
            {
                throw new NotFoundException($"Project with id {id} not found");
            }

            // Get all associated tasks
            IEnumerable<TaskEntity> associatedTasks = await _taskRepository.GetAllTasksFilteredAsync(new() { ProjectId = id });

            if (!forceTaskDeleting)
            {
                // Check if all tasks.Status are in ("Completed", "Closed", "Undefined")
                // Get all uncompleted associated tasks
                List<TaskEntity> uncompletedTasksList = associatedTasks.Where(x => x.Status != StatusEnum.Completed && x.Status != StatusEnum.Undefined).ToList();

                if (uncompletedTasksList.Any())
                {
                    throw new UncompletedTasksAssociated("There are still uncompleted associated tasks to the project. All associated tasks must be at a completed status.");
                }
                else
                {
                    // If all tasks are completed, delete all associated tasks before deleting the project
                    foreach (TaskEntity task in uncompletedTasksList)
                    {
                        await _taskRepository.DeleteTaskAsync(task);
                    }
                }
            }
            else
            {
                // Delete all associated tasks, whatever the task.Status
                foreach (TaskEntity task in associatedTasks)
                    await _taskRepository.DeleteTaskAsync(task);
            }

            // Delete project
            bool deleted = await _projectRepository.DeleteProjectAsync(projectEntity);
            if (!deleted)
            {
                throw new Exception($"Error deleting the project with id {id}");
            }

            return deleted;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProjectDto>> GetAllProjectsFilteredAsync(string? title, string? description, StatusEnum? status)
        {
            var filter = new ProjectQueryFilter
            {
                Title = title,
                Description = description,
                Status = status
            };

            IEnumerable<ProjectEntity> entities = await _projectRepository.GetAllProjectsFilteredAsync(filter);
            List<ProjectDto> projectDtoList = entities.Select(ProjectMapper.ToDto).ToList();
            return projectDtoList;
        }


        /// <inheritdoc/>
        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            ProjectEntity? entity = await _projectRepository.GetProjectByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Project with id {id} not foud");
            }

            ProjectDto projectDto = ProjectMapper.ToDto(entity);
            return projectDto;
        }

        /// <inheritdoc/>
        public async Task<ProjectDto> UpdateProjectAsync(int id, UpdateProjectDto updatedProject)
        {
            ProjectEntity? existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                throw new NotFoundException($"Project with id {id} not foud");
            }

            ProjectEntity updatedEntity = ProjectMapper.ToUpdateEntity(existingProject, updatedProject);

            updatedEntity = await _projectRepository.UpdateProjectAsync(existingProject, updatedEntity);
            var projectDto = ProjectMapper.ToDto(updatedEntity);
            return projectDto;
        }
        #endregion
    }
}

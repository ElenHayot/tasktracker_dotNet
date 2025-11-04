using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Mappers;
using tasktracker.Repositories;
using tasktracker.Exceptions;
using tasktracker.Enums;

namespace tasktracker.Services
{
    /// <summary>
    /// Project service - manage Projects calls
    /// </summary>
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// Local project repository instance
        /// </summary>
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// ProjectService constructor
        /// </summary>
        /// <param name="projectRepository">Project repository instance</param>
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <inheritdoc/>
        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto project)
        {

            ProjectEntity entity = ProjectMapper.ToCreateEntity(project);
            var createdEntity = await _projectRepository.CreateProjectAsync(entity);
            ProjectDto projectDto = ProjectMapper.ToDto(createdEntity);
            return projectDto;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProjectAsync(int id)
        {
            ProjectEntity? projectEntity = await _projectRepository.GetProjectByIdAsync(id);
            if (projectEntity == null)
            {
                throw new NotFoundException($"Project with id {id} not foud");
            }

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
        public async Task<ProjectDto> UpdateProjectAsync(int id, CreateProjectDto updatedProject)
        {
            ProjectEntity? existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                throw new NotFoundException($"Project with id {id} not foud");
            }

            ProjectEntity updatedEntity = new ()
            {
                Id = id,
                Title = updatedProject.Title,
                Description = updatedProject.Description,
                Status = updatedProject.Status,
                UpdatedAt = DateTime.UtcNow
            };

            updatedEntity = await _projectRepository.UpdateProjectAsync(existingProject, updatedEntity);
            var projectDto = ProjectMapper.ToDto(updatedEntity);
            return projectDto;
        }
    }
}

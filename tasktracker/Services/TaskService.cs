using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Mappers;
using tasktracker.Repositories;

namespace tasktracker.Services
{
    /// <summary>
    /// Task service - manage Tasks calls
    /// </summary>
    public class TaskService : ITaskService
    {
        /// <summary>
        /// Local task repository instance
        /// </summary>
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Local project repository instance
        /// </summary>
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Local user repository instance
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Local logger instance for TaskService
        /// </summary>
        private readonly ILogger<TaskService> _logger;

        /// <summary>
        /// TaskService constructor
        /// </summary>
        /// <param name="taskRepository">Task repository instance</param>
        /// <param name="projectRepository">Project repository instance</param>
        /// <param name="userRepository">User repository instance</param>
        /// <param name="logger">Task service logger instance</param>
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto task)
        {
            // Verify existing project
            ProjectEntity? existingProject = await _projectRepository.GetProjectByIdAsync(task.ProjectId);
            if (existingProject == null)
            {
                throw new AssociatedProjectNotFound($"Project ID '{task.ProjectId}' not found. Task must be affected to a valid project.");
            }

            // Verify existing user
            UserEntity? existingUser = await _userRepository.GetUserByIdAsync(task.UserId);

            if (task.UserId != 0 && existingUser == null)
            {
                throw new AssociatedUserNotFound($"User ID '{task.UserId}' not found. Associated user must exist. Set 0 to associate no user.");
            }

            // Add task in DB
            TaskEntity newTaskEntity = TaskMapper.ToCreateEntity(task);
            // Get newTaskEntity to know the new task ID
            newTaskEntity = await _taskRepository.CreateTaskAsync(newTaskEntity);
            TaskDto createdTaskDto = TaskMapper.ToDto(newTaskEntity);

            // Add task ID to project.TaskIds
            await AddTaskToProject(createdTaskDto.Id, existingProject);

            // Add task ID to user.TaskIds
            if (existingUser != null)
            {
                await AddTaskToUser(createdTaskDto.Id, existingUser);
            }

            return createdTaskDto;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            TaskEntity? task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                throw new NotFoundException($"Task with id '{id}' not found.");
            }

            // Update associated project TaskIds list
            // if existingProject == null -> add log info and continue
            ProjectEntity? existingProject = await _projectRepository.GetProjectByIdAsync(task.ProjectId);
            if (existingProject != null)
            {
                await RemoveTaskFromProject(task.Id, existingProject);
            }
            else
                _logger.LogInformation($"DeleteTaskAsync - Remove from project : Project with id '{task.ProjectId}' not found - ignored");

            // Update associated user TaskIds list if userId != 0
            UserEntity? associatedUser = await _userRepository.GetUserByIdAsync(task.UserId);
            if (associatedUser != null)
            {
                await RemoveTaskFromUser(task.Id, associatedUser);
            }
            else
                _logger.LogInformation($"DeleteTaskAsync - Remove from user : User with id '{task.UserId}' not found - ignored");

                // Delete task in DB
                bool deleted = await _taskRepository.DeleteTaskAsync(task);
            if (!deleted)
            {
                throw new Exception($"Error deleting the task with id {id}");
            }

            return deleted;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TaskDto>> GetAllTasksFilteredAsync(string? title, string? description, int? projectId, int? userId, StatusEnum? status)
        {
            TaskQueryFilter filter = new()
            {
                Title = title,
                Description = description,
                ProjectId = projectId,
                UserId = userId,
                Status = status
            };

            IEnumerable<TaskEntity> entities = await _taskRepository.GetAllTasksFilteredAsync(filter);
            List<TaskDto> tasks = entities.Select(TaskMapper.ToDto).ToList();
            return tasks;
        }

        /// <inheritdoc/>
        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            TaskEntity? entity = await _taskRepository.GetTaskByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Task with id '{id}' not found.");
            }

            TaskDto task = TaskMapper.ToDto(entity);
            return task;
        }

        /// <inheritdoc/>
        public async Task<TaskDto> UpdateTaskAsync(int id, UpdateTaskDto updatedTask)
        {
            TaskEntity? existingEntity = await _taskRepository.GetTaskByIdAsync(id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Task with id '{id}' not found.");
            }


            // Build new task data object
            TaskEntity updatedEntity = TaskMapper.ToUpdateEntity(existingEntity, updatedTask);

            // Update TaskIds list for user if different UserId
            if (existingEntity.UserId != updatedEntity.UserId)
            {
                // Remove from old user if old user != 0
                if (existingEntity.UserId != 0)
                {
                    UserEntity? oldAssociatedUser = await _userRepository.GetUserByIdAsync(existingEntity.UserId);

                    if (oldAssociatedUser != null)
                    {
                        await RemoveTaskFromUser(existingEntity.Id, oldAssociatedUser);
                    }
                    else
                        _logger.LogInformation($"RemoveTaskFromUser : User with id '{existingEntity.UserId}' not found - ignored");
                }

                // Add to new user if new user != 0
                if (updatedEntity.UserId != 0)
                {
                    UserEntity? newAssociatedUser = await _userRepository.GetUserByIdAsync(updatedEntity.UserId);
                    if (newAssociatedUser == null)
                    {
                        throw new AssociatedUserNotFound($"User ID '{updatedEntity.UserId}' not found. Associated user must exist. Set 0 to associate no user.");
                    }

                    await AddTaskToUser(updatedEntity.Id, newAssociatedUser);
                }
            }

            await _taskRepository.UpdateTaskAsync(existingEntity, updatedEntity);

            return TaskMapper.ToDto(updatedEntity);
        }

        /// <summary>
        /// Remove a task ID from a user TaskIds list
        /// </summary>
        /// <param name="taskId">Task ID to remove</param>
        /// <param name="user">User to update</param>
        /// <returns></returns>
        private async Task RemoveTaskFromUser(int taskId, UserEntity user)
        {
            user.TaskIds?.Remove(taskId);
            await _userRepository.SaveUpdatesAsync(user);
        }

        /// <summary>
        /// Add a task ID to a user TaskIds list
        /// </summary>
        /// <param name="taskId">Task ID to add</param>
        /// <param name="user">User to update</param>
        /// <returns></returns>
        private async Task AddTaskToUser(int taskId, UserEntity user)
        {
            user.TaskIds?.Add(taskId);
            await _userRepository.SaveUpdatesAsync(user);
        }

        /// <summary>
        /// Remove a task ID from a project TaskIds list
        /// </summary>
        /// <param name="taskId">Task ID to remove</param>
        /// <param name="project">Projec to update</param>
        /// <returns></returns>
        private async Task RemoveTaskFromProject(int taskId, ProjectEntity project)
        {
            project.TaskIds?.Remove(taskId);
            await _projectRepository.SaveUpdatesAsync(project);
        }

        /// <summary>
        /// Add a task ID to a project TaskIds list
        /// </summary>
        /// <param name="taskId">Task ID to add</param>
        /// <param name="project">Project to update</param>
        /// <returns></returns>
        private async Task AddTaskToProject(int taskId, ProjectEntity project)
        {
            project.TaskIds?.Add(taskId);
            await _projectRepository.SaveUpdatesAsync(project);
        }
    }
}

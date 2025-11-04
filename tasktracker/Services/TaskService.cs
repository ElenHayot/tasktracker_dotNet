using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Mappers;
using tasktracker.Repositories;
using tasktracker.Exceptions;

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
        /// TaskService constructor
        /// </summary>
        /// <param name="taskRepository">Task repository instance</param>
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto task)
        {
            TaskEntity taskToAdd = TaskMapper.ToCreateEntity(task);
            taskToAdd = await _taskRepository.CreateTaskAsync(taskToAdd);
            TaskDto createdTask = TaskMapper.ToDto(taskToAdd);
            return createdTask;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            TaskEntity? task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                throw new NotFoundException($"Task with id '{id}' not found.");
            }

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
        public async Task<TaskDto> UpdateTaskAsync(int id, CreateTaskDto updatedTask)
        {
            TaskEntity? existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                throw new NotFoundException($"Task with id '{id}' not found.");
            }

            TaskEntity updatedEntity = new()
            {
                Id = id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                ProjectId = updatedTask.ProjectId,
                UserId = updatedTask.UserId,
                Status = updatedTask.Status,
                CreatedAt = existingTask.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = existingTask.CreatedBy,
                UpdatedBy = updatedTask.UpdatedBy
            };

            updatedEntity = await _taskRepository.UpdateTaskAsync(existingTask, updatedEntity);
            return TaskMapper.ToDto(updatedEntity);
        }
    }
}

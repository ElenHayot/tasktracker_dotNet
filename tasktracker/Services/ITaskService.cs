using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Services
{
    /// <summary>
    /// Task service interface
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Get all tasks with applied filter
        /// </summary>
        /// <param name="title">Filter LIKE on title</param>
        /// <param name="description">Filter LIKE on description</param>
        /// <param name="projectId">Filter on associated project ID</param>
        /// <param name="userId">Filter on associated user ID</param>
        /// <param name="status">Filter on the task status</param>
        /// <returns>List of tasks (DTO)</returns>
        Task<IEnumerable<TaskDto>> GetAllTasksFilteredAsync(string? title, string? description, int? projectId, int? userId, StatusEnum? status);

        /// <summary>
        /// Get an existing task by its ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>One task (DTO)</returns>
        Task<TaskDto> GetTaskByIdAsync(int id);

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="task">Task to create</param>
        /// <returns>The created task (DTO)</returns>
        Task<TaskDto> CreateTaskAsync(CreateTaskDto task);

        /// <summary>
        /// Update an existing task
        /// </summary>
        /// <param name="id">Task's ID to update</param>
        /// <param name="updatedTask">New datas</param>
        /// <returns>The updated task (DTO)</returns>
        Task<TaskDto> UpdateTaskAsync(int id, CreateTaskDto updatedTask);

        /// <summary>
        /// Delete an existing task
        /// </summary>
        /// <param name="id">Task's ID to delete</param>
        /// <returns>true/false</returns>
        Task<bool> DeleteTaskAsync(int id);
    }
}

using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Get all tasks in DB with applied filter
        /// </summary>
        /// <param name="filter">Filter to apply</param>
        /// <returns>List of tassks (entities)</returns>
        Task<IEnumerable<TaskEntity>> GetAllTasksFilteredAsync(TaskQueryFilter filter);

        /// <summary>
        /// Get an existing task by its ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>A task (entity)</returns>
        Task<TaskEntity?> GetTaskByIdAsync(int id);

        /// <summary>
        /// Add a new task in DB
        /// </summary>
        /// <param name="task">Task to create</param>
        /// <returns>The created task (entity)</returns>
        Task<TaskEntity> CreateTaskAsync(TaskEntity task);

        /// <summary>
        /// Update an existing task
        /// </summary>
        /// <param name="existingTask">Existing task</param>
        /// <param name="updatedTask">New datas</param>
        /// <returns>The updated task</returns>
        Task<TaskEntity> UpdateTaskAsync(TaskEntity existingTask, TaskEntity updatedTask);

        /// <summary>
        /// Delete an existing task
        /// </summary>
        /// <param name="task">Task to delete</param>
        /// <returns>true/false</returns>
        Task<bool> DeleteTaskAsync(TaskEntity task);
    }
}

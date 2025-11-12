using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Mappers
{
    /// <summary>
    /// Mapper management for Tasks
    /// </summary>
    public class TaskMapper
    {
        /// <summary>
        /// Mapper CreateTaskDto -> TaskEntity for creates
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>TaskEntity object</returns>
        public static TaskEntity ToCreateEntity(CreateTaskDto dto)
        {
            return new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Comment = dto.Comment,
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "0",
                UpdatedBy = dto.UpdatedBy ?? "0"
            };
        }

        /// <summary>
        /// Mapper UpdateTaskDto -> TaskEntity for updates
        /// </summary>
        /// <param name="existingTask">Existing task in DB</param>
        /// <param name="updatedTask">New data to set</param>
        /// <returns>TaskEntity object</returns>
        public static TaskEntity ToUpdateEntity(TaskEntity existingTask, UpdateTaskDto updatedTask)
        {
            return new TaskEntity
            {
                Id = existingTask.Id,
                Title = updatedTask.Title ?? existingTask.Title,
                Description = updatedTask.Description ?? existingTask.Description,
                Comment = updatedTask.Comment ?? existingTask.Comment,
                ProjectId = existingTask.ProjectId,
                UserId = updatedTask.UserId ?? existingTask.UserId,
                Status = updatedTask.Status ?? existingTask.Status,
                CreatedAt = existingTask.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = existingTask.CreatedBy,
                UpdatedBy = updatedTask.UpdatedBy
            };
        }

        /// <summary>
        /// Mapper TaskDto -> TaskEntity
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>TaskEntity object</returns>
        public static TaskEntity ToEntity(TaskDto dto)
        {
            return new TaskEntity
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Comment = dto.Comment,
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy
            };
        }

        /// <summary>
        /// Mapper TaskEntity -> TaskDto
        /// </summary>
        /// <param name="entity">entity to map</param>
        /// <returns>TaskDto object</returns>
        public static TaskDto ToDto(TaskEntity entity)
        {
            return new TaskDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Comment = entity.Comment,
                ProjectId = entity.ProjectId,
                UserId = entity.UserId,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}

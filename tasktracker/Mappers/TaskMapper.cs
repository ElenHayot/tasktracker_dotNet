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
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "0",
                UpdatedBy = "0"
            };
        }

        /// <summary>
        /// Mapper CreateTaskDto -> TaskEntity for updates
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>TaskEntity object</returns>
        public static TaskEntity ToUpdateEntity(CreateTaskDto dto)
        {
            return new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = dto.UpdatedBy,
                UpdatedBy = "0"
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

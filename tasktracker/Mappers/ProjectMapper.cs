using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Mappers
{
    /// <summary>
    /// Mapper management for Projects
    /// </summary>
    public class ProjectMapper
    {
        /// <summary>
        /// Mapper CreateProjectDto -> ProjectEntity
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>ProjectEntity object</returns>
        public static ProjectEntity ToCreateEntity(CreateProjectDto dto)
        {
            return new ProjectEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "0",
                UpdatedBy = "0"
            };
        }

        /// <summary>
        /// Mapper ProjectDto -> ProjectEntity
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>ProjectEntity object</returns>
        public static ProjectEntity ToEntity(ProjectDto dto)
        {
            return new ProjectEntity
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = dto.Id.ToString()
            };
        }

        /// <summary>
        /// Mapper ProjectEntity -> ProjectDto
        /// </summary>
        /// <param name="entity">entity to map</param>
        /// <returns>ProjectDto object</returns>
        public static ProjectDto ToDto(ProjectEntity entity)
        {
            return new ProjectDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}

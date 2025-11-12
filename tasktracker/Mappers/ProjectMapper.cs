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
                Comment = dto.Comment,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "0",
                UpdatedBy = dto.UpdatedBy ?? "0"
            };
        }

        /// <summary>
        /// Mapper UpdateProjectDto -> ProjectEntity
        /// </summary>
        /// <param name="existingProject">Existing project in DB</param>
        /// <param name="updatedProjectDto">New data to set</param>
        /// <returns>ProjectEntity object</returns>
        public static ProjectEntity ToUpdateEntity(ProjectEntity existingProject, UpdateProjectDto updatedProjectDto)
        {
            return new ProjectEntity
            {
                Id = existingProject.Id,
                Title = updatedProjectDto.Title ?? existingProject.Title,
                Description = updatedProjectDto.Description ?? existingProject.Description,
                Comment = updatedProjectDto.Comment ?? existingProject.Comment,
                Status = updatedProjectDto.Status ?? existingProject.Status,
                TaskIds = updatedProjectDto.TaskIds ?? existingProject.TaskIds,
                CreatedAt = existingProject.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = existingProject.CreatedBy,
                UpdatedBy = updatedProjectDto.UpdatedBy
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
                Comment = dto.Comment,
                Status = dto.Status,
                TaskIds = dto.TaskIds,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy
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
                Comment = entity.Comment,
                Status = entity.Status,
                TaskIds = entity.TaskIds,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}

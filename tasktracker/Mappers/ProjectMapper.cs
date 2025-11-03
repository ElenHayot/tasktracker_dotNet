using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Mappers
{
    public class ProjectMapper
    {
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

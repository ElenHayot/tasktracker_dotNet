using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model Project
    /// </summary>
    public class ProjectDto : HandlingDto
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(240)]
        public string? Description { get; set; }

        [Required]
        public required StatusEnum Status { get; set; } = StatusEnum.Undefined;
    }
}

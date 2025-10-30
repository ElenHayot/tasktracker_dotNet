using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// Modèle DTO de création de project - sans Id : autoincrémenté en base
    /// </summary>
    public class CreateProjectDto
    {
        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }
        [MaxLength(240)]
        public string? Description { get; set; }
        [Required]
        public required StatusEnum Status { get; set; } = StatusEnum.Undefined;
    }
}

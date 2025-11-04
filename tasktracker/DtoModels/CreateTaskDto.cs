using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to create (or update) a task - no ID field
    /// </summary>
    public class CreateTaskDto : HandlingDto
    {
        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(240)]
        public string? Description { get; set; }

        [Required]
        public required int ProjectId { get; set; }

        public int? UserId { get; set; } = 0;

        [Required]
        public required StatusEnum Status { get; set; } = StatusEnum.Undefined;
    }
}

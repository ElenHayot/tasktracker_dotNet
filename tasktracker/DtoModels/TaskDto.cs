using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    public class TaskDto : HandlingDto
    {
        [Required]
        public required int Id { get; set; }

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

using System.ComponentModel.DataAnnotations;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Entities
{
    public class ProjectEntity : HandlingDto
    {
        [Key]
        public required int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public required StatusEnum Status { get; set; }
    }
}

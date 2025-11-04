using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Entities
{
    /// <summary>
    /// Entity model for Tasks table
    /// </summary>
    public class TaskEntity : HandlingDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public required int ProjectId { get; set; }

        public int? UserId { get; set; }

        [Required]
        public required StatusEnum Status { get; set; }
    }
}

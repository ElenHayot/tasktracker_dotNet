using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Entities
{
    /// <summary>
    /// Entity model for Projects table
    /// </summary>
    public class ProjectEntity : HandlingDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public required StatusEnum Status { get; set; }
    }
}

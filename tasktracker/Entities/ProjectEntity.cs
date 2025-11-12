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
        /// <summary>
        /// Projects table PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Project's title
        /// </summary>
        [Required]
        public required string Title { get; set; }

        /// <summary>
        /// Project's description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Project's additional comment
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Project's status
        /// </summary>
        [Required]
        public required StatusEnum Status { get; set; }

        /// <summary>
        /// Project's associated tasks (IDs)
        /// </summary>
        public List<int>? TaskIds { get; set; } = [];
    }
}

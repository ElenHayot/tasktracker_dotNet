using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Entities
{
    /// <summary>
    /// Entity model for Tasks table
    /// </summary>
    [Index(nameof(Title), IsUnique = true)]
    public class TaskEntity : HandlingDto
    {
        /// <summary>
        /// Tasks table PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Task's title
        /// </summary>
        [Required]
        public required string Title { get; set; }

        /// <summary>
        /// Task's description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Task's additional comment
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Task's associated project (ID)
        /// Must be an existing project
        /// </summary>
        [Required]
        public required int ProjectId { get; set; }

        /// <summary>
        /// Task's associated user 
        /// if 0 : no user affected
        /// else : must be an existing user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Task's status
        /// </summary>
        [Required]
        public required StatusEnum Status { get; set; }
    }
}

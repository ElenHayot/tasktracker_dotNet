using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to create a task - no ID field
    /// </summary>
    public class CreateTaskDto : HandlingDto
    {
        /// <summary>
        /// Task title
        /// </summary>
        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [MaxLength(240)]
        public string? Description { get; set; }

        /// <summary>
        /// Task additional comment
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Task associated project ID
        /// </summary>
        [Required]
        public required int ProjectId { get; set; }

        /// <summary>
        /// Task associated user ID
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Task status - default "Undefined"
        /// </summary>
        [Required]
        public required StatusEnum Status { get; set; } = StatusEnum.Undefined;
    }
}

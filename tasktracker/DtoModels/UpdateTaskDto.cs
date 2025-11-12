using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to update a task
    /// </summary>
    public class UpdateTaskDto : HandlingDto
    {
        /// <summary>
        /// Task title
        /// </summary>
        [MaxLength(50)]
        public string? Title { get; set; }

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
        /// Task associated user ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public StatusEnum? Status { get; set; }
    }
}

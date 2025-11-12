using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model without ID field (used to create or update an entity)
    /// </summary>
    public class CreateProjectDto : HandlingDto
    {
        /// <summary>
        /// Project title
        /// </summary>
        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }
        
        /// <summary>
        /// Project description
        /// </summary>
        [MaxLength(240)]
        public string? Description { get; set; }

        /// <summary>
        /// Project additional comment
        /// </summary>
        [MaxLength(150)]
        public string? Comment { get; set; }

        /// <summary>
        /// Project status - default "Undefined"
        /// </summary>
        [Required]
        public required StatusEnum Status { get; set; } = StatusEnum.Undefined;
    }
}

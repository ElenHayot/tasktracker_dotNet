using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to update a project
    /// </summary>
    public class UpdateProjectDto : HandlingDto
    {
        /// <summary>
        /// Project title
        /// </summary>
        [MaxLength(50)]
        public string? Title { get; set; }

        /// <summary>
        /// Project description
        /// </summary>
        [MaxLength(240)]
        public string? Description { get; set; }

        /// <summary>
        /// Project odditional comment
        /// </summary>
        [MaxLength(150)]
        public string? Comment { get; set; }

        /// <summary>
        /// Project status - default "Undefined"
        /// </summary>
        public StatusEnum? Status { get; set; }

        /// <summary>
        /// Project associated tasks (IDs)
        /// </summary>
        public List<int>? TaskIds { get; set; }
    }
}

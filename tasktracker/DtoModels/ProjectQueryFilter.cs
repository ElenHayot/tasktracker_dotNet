using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for Projects
    /// </summary>
    public class ProjectQueryFilter
    {
        /// <summary>
        /// Query on project title field
        /// </summary>
        public string? Title {  get; set; }
        
        /// <summary>
        /// Query on project description field
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Query on project status field
        /// </summary>
        public StatusEnum? Status { get; set; }
    }
}

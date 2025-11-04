using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for Projects
    /// </summary>
    public class ProjectQueryFilter
    {
        public string? Title {  get; set; }
        public string? Description { get; set; }
        public StatusEnum? Status { get; set; }
    }
}

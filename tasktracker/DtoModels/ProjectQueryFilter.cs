using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    public class ProjectQueryFilter
    {
        public string? Title {  get; set; }
        public string? Description { get; set; }
        public StatusEnum? Status { get; set; }
    }
}

using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for Task
    /// </summary>
    public class TaskQueryFilter
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? ProjectId { get; set; }

        public int? UserId { get; set; }

        public StatusEnum? Status { get; set; }
    }
}

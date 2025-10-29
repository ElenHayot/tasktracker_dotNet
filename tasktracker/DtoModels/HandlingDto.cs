namespace tasktracker.DtoModels
{
    public class HandlingDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
    }
}

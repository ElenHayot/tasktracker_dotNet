namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to manage handling - common to DTO and entity models
    /// </summary>
    public class HandlingDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
    }
}

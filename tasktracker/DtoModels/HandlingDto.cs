namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to manage handling - common to DTO and entity models
    /// </summary>
    public class HandlingDto
    {
        /// <summary>
        /// Creation datetime
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Creator ID
        /// </summary>
        public string? CreatedBy { get; set; }
        
        /// <summary>
        /// Updating datetime
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        
        /// <summary>
        /// Updator ID
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}

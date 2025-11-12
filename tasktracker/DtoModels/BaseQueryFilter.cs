namespace tasktracker.DtoModels
{
    /// <summary>
    /// Query filter base - common to all filter types
    /// </summary>
    public abstract class BaseQueryFilter
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int? Page { get; set; }
        
        /// <summary>
        /// Number of items per page
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Field sorting by
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort descending PK (default false)
        /// </summary>
        public bool SortDescending { get; set; }
    }
}

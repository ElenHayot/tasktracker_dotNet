namespace tasktracker.DtoModels
{
    /// <summary>
    /// Query filter base - common to all filter types
    /// </summary>
    public abstract class BaseQueryFilter
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
}

namespace tasktracker.DtoModels
{
    /// <summary>
    /// Classe pour les filtres de bases communs à tous les filtres services
    /// </summary>
    public abstract class BaseQueryFilter
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
}

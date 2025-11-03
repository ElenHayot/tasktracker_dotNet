namespace tasktracker.Repositories
{
    public interface ICommonRepository
    {
        /// <summary>
        /// Apply a filter on a query
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="query">Query to filter</param>
        /// <param name="filter">Filter to apply</param>
        /// <returns></returns>
        IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Object filter);
    }
}

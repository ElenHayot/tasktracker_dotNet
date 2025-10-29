namespace tasktracker.Repositories
{
    public interface ICommonRepository
    {
        IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Object filter);
    }
}

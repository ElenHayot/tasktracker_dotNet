
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace tasktracker.Repositories
{
    /// <summary>
    /// Common repository - manage common DB functions
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        /// <summary>
        /// CommonRepository constructor
        /// </summary>
        public CommonRepository()
        {
        }

        /// <inheritdoc/>
        public IQueryable<T> ApplyFilter<T>(IQueryable<T> query, object filter)
        {

            foreach (PropertyInfo prop in filter.GetType().GetProperties()) 
            {
                var value = prop.GetValue(filter);
                var ignoredProps = new[] { "Page", "PageSize", "SortBy", "SortDescending" };
                if (value ==  null) continue;
                if (ignoredProps.Contains(prop.Name)) continue;

                // Ajoute dynamiquement un filtre, ex: "Name.Contains(@0)" ou "Age == @0"
                if (prop.PropertyType == typeof(string))
                    query = query.Where($"{prop.Name}.Contains(@0)", value);
                else
                    query = query.Where($"{prop.Name} == @0", value);
            }

            // Appliquer les SortField (ignoredProps) sur query

            return query.AsQueryable();
        }
    }
}

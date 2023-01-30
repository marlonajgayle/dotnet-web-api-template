using Net7WebApiTemplate.Application.Shared.Models;

namespace Net7WebApiTemplate.Application.Shared.Extensions
{
    public static class PaginationExtension
    {
        public static ValueTask<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable,
            int offset, int limit)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, offset, limit);
        }
    }
}
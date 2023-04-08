using NetWebApiTemplate.Application.Shared.Models;

namespace NetWebApiTemplate.Application.Shared.Extensions
{
    public static class PaginationExtension
    {
        public static ValueTask<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable,
            int offset, int limit, CancellationToken cancellationToken)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, offset, limit, cancellationToken);
        }
    }
}
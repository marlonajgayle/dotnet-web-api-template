using Microsoft.EntityFrameworkCore;

namespace Net7WebApiTemplate.Application.Shared.Models
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public List<T> Items { get; } = new List<T>();
        public bool HasPerviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < PageCount;

        public PaginatedList(int currentPage, int pageSize, int count, List<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public static async ValueTask<PaginatedList<T>> CreateAsync(IQueryable<T> query, int currentPage, int pageSize,
            CancellationToken cancellationToken)
        {
            int count = await query.CountAsync();
            var items = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<T>(currentPage, pageSize, count, items);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface INet7WebApiTemplateDbContext
    {
        DbSet<Faq> Faqs { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCatergory> ProductCatergories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
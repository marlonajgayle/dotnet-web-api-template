using Microsoft.EntityFrameworkCore;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Application.Shared.Interface
{
    public interface INetWebApiTemplateDbContext
    {
        DbSet<Faq> Faqs { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
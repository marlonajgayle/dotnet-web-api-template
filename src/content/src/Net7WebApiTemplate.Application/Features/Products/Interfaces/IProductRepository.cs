using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Application.Features.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetById(long id);
        Task Update(Product product);
    }
}
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Application.Features.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetById(long id);
        Task Update(Product product);
    }
}
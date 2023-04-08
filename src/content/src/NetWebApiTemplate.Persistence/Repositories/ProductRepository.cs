using Dapper;
using NetWebApiTemplate.Application.Features.Products.Interfaces;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ProductRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product?> GetById(long id)
        {
            var sql = @"SELECT ProductID, ProductName, ProductDescription, ProductPrice
                        FROM Products p
                        INNER JOIN ProductCategories pc ON p.CategoryID
                        WHERE ProductID = @ProductId"
            ;

            using var sqlconnection = _connectionFactory.CreateConnection();
            var entity = await sqlconnection.QueryAsync<Product, ProductCatergory, Product>
                (sql, (product, productCategory) =>
                {
                    product.ProductCatergory = productCategory;
                    return product;
                },
                splitOn: "CategoryID");

            if (entity == null)
            {
                return null;
            }

            return entity.FirstOrDefault();
        }

        public async Task Update(Product product)
        {
            using var sqlconnection = _connectionFactory.CreateConnection();
            await sqlconnection.ExecuteAsync(
                @"UPDATE Products
                 SET @ProductName, @ProductDescription, @ProductPrice
                 WHERE ProductID = @ProductId",
                new { product.ProductId, product.ProductName, product.ProductDescription, product.ProductPrice });
        }
    }
}
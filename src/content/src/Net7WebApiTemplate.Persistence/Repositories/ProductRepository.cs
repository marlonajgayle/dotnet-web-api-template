using Dapper;
using Microsoft.Data.SqlClient;
using Net7WebApiTemplate.Application.Features.Products.Interfaces;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperDbContext _dbContext;
        public ProductRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetById(long id)
        {
            var sql = @"SELECT ProductID, ProductName, ProductDescription, ProductPrice
                        FROM Products p
                        INNER JOIN ProductCategories pc ON p.CategoryID
                        WHERE ProductID = @ProductId";

            await using SqlConnection sqlconnection = _dbContext.CreateConnection();

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
            await using SqlConnection sqlconnection = _dbContext.CreateConnection();
            await sqlconnection.ExecuteAsync(
                @"UPDATE Products
                 SET @ProductName, @ProductDescription, @ProductPrice
                 WHERE ProductID = @ProductId",
                new { product.ProductId, product.ProductName, product.ProductDescription, product.ProductPrice });
        }
    }
}
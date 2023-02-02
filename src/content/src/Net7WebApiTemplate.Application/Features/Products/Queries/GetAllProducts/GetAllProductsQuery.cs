using Mediator;
using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Application.Shared.Extensions;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Application.Shared.Models;

namespace Net7WebApiTemplate.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<PaginatedList<ProductsDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<ProductsDto>>
    {
        private readonly INet7WebApiTemplateDbContext _dbContext;

        public GetAllProductsQueryHandler(INet7WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<PaginatedList<ProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .Select(p => new ProductsDto
                {
                    Id = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.ProductPrice
                })
                .OrderBy(p => p.ProductName)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            return products;
        }
    }
}
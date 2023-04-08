using Mediator;
using Microsoft.EntityFrameworkCore;
using NetWebApiTemplate.Application.Shared.Extensions;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Application.Shared.Models;

namespace NetWebApiTemplate.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<PaginatedList<ProductsDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<ProductsDto>>
    {
        private readonly INetWebApiTemplateDbContext _dbContext;

        public GetAllProductsQueryHandler(INetWebApiTemplateDbContext dbContext)
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
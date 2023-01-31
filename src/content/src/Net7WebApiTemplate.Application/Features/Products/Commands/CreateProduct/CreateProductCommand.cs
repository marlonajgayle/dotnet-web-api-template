using Mediator;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Application.Features.Products.Commands
{
    public record CreateProductCommand : IRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly INet7WebApiTemplateDbContext _dbContext;

        public CreateProductCommandHandler(INet7WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var entity = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice
            };

            _dbContext.Products.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
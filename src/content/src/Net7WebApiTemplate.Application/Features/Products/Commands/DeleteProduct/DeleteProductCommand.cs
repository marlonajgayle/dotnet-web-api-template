using Mediator;
using Net7WebApiTemplate.Application.Shared.Exceptions;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly INet7WebApiTemplateDbContext _dbContext;

        public DeleteProductCommandHandler(INet7WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Products.FirstOrDefault(p => p.ProductId == request.Id);

            if (entity == null) 
            {
                throw new NotFoundException();
            }

            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
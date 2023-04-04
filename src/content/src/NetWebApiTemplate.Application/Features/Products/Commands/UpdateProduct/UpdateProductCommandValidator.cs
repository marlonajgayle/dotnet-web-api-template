using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("ProductId value is required.");

            RuleFor(v => v.ProductName)
                .NotEmpty().WithMessage("Product Name field is required.");

            RuleFor(v => v.ProductDescription)
                .NotEmpty().WithMessage("Product Name field is required.");

            RuleFor(v => v.ProductPrice)
                .NotEmpty().WithMessage("Product Name field is required.");
        }
    }
}
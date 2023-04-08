using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.ProductName)
                .NotEmpty().WithMessage("Product name field is required!")
                .MaximumLength(100).WithMessage("Product name has a maximum field size of 100 characters.");

            RuleFor(v => v.ProductDescription)
                .NotEmpty().WithMessage("Product description field is required!")
                .MaximumLength(100).WithMessage("Product description has a maximum field size of 500 characters.");

            RuleFor(v => v.ProductPrice)
                .NotEmpty().WithMessage("Product price field is required!")
                .GreaterThan(0).WithMessage("");
        }
    }
}
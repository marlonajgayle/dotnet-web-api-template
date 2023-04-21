using FluentValidation;

namespace __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Queries.__QUERY_NAME__
{
    public class __QUERY_NAME__QueryValidator : AbstractValidator<__QUERY_NAME__Query>
    {
        public __QUERY_NAME__QueryValidator()
        {
            RuleFor(v => v.Property1).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Property1 field is required.");
        }
    }
}
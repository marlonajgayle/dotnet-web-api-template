using FluentValidation;

namespace Net7WebApiTemplate.Application.Features.Authentication.Queries.GetUserClaims
{
    public class GetUserClaimsQueryValidator : AbstractValidator<GetUserClaimsQuery>
    {
        public GetUserClaimsQueryValidator()
        {
            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
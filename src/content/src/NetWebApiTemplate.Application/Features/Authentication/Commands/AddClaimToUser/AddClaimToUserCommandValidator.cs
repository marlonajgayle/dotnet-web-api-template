using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.AddClaimToUser
{
    public class AddClaimToUserCommandValidator : AbstractValidator<AddClaimToUserCommand>
    {
        public AddClaimToUserCommandValidator()
        {
            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(v => v.ClaimName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Claim name field is required.");

            RuleFor(v => v.ClaimValue).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Claim value field is required.");
        }
    }
}
using FluentValidation;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email field required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password field is required.");
        }
    }
}
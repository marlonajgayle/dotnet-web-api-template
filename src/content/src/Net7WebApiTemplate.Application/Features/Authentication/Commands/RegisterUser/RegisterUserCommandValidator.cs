using FluentValidation;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("Firstname field is required.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Lastname field is required.");

            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password field is required.");
        }
    }
}
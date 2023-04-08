using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.AddUserToRole
{
    public class AddUserToRoleCommandValidator : AbstractValidator<AddUserToRoleCommand>
    {
        public AddUserToRoleCommandValidator()
        {
            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(v => v.RoleName)
                .NotEmpty().WithMessage("Role name field is required.");
        }
    }
}
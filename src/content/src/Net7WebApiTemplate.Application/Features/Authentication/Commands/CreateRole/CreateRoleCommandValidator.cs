using FluentValidation;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(v => v.RoleName)
                .NotEmpty().WithMessage("Role Name field is required.");
        }
    }
}
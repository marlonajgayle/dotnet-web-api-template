using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.CreateRole
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
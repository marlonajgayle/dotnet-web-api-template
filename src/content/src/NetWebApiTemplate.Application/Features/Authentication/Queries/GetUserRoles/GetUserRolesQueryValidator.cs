using FluentValidation;

namespace NetWebApiTemplate.Application.Features.Authentication.Queries.GetUserRoles
{
    public class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesQuery>
    {
        public GetUserRolesQueryValidator()
        {
            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email address format.");
        }
    }
}
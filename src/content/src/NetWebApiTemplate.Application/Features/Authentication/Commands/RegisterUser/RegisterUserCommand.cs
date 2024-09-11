using MediatR;
using NetWebApiTemplate.Application.Features.Authentication.Interfaces;
using NetWebApiTemplate.Application.Features.Authentication.Models;
using NetWebApiTemplate.Application.Shared.Exceptions;
using NetWebApiTemplate.Application.Shared.Interface;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IOutboxService _outboxService;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService, 
            IOutboxService outboxService)
        {
            _authenticationService = authenticationService;
            _outboxService = outboxService;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var result = await _authenticationService.RegisterUserAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("failed to register user.");
            }

            // store domain event to outbox
            await _outboxService.StoreDomainEvent(new UserRegisteredEvent
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            }, cancellationToken);

           
        }
    }
}
using Mediator;
// using __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Interfaces;

namespace __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Commands.__COMMAND_NAME__
{
    public record __COMMAND_NAME__Command : IRequest
    {
        public string Property1 { get; set; } = string.Empty;
        public string Property2 { get; set; } = string.Empty;
        public string Property3 { get; set; } = string.Empty;
    }

    public class __COMMAND_NAME__CommandHandler : IRequestHandler<__COMMAND_NAME__Command>
    {
        // private readonly IAuthenticationService _authenticationService;

        public __COMMAND_NAME__CommandHandler(/*IAuthenticationService authenticationService*/)
        {
            // _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(__COMMAND_NAME__Command request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Property1: {request.Property1}");
            Console.WriteLine($"Property2: {request.Property2}");
            Console.WriteLine($"Property3: {request.Property3}");

            return Unit.Value;
        }
    }
}

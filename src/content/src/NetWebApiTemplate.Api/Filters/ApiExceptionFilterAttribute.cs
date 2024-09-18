using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetWebApiTemplate.Application.Shared.Exceptions;
using Sentry;

namespace NetWebApiTemplate.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;


        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(ForbiddenException), HandleForbiddenException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedException), HandleUnauthorizedException },
                { typeof(ValidationException), HandleValidationException }

            };

            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);

            // log the exception
            // NB: Removed this line since sentry is capturing exceptions
            // _logger.LogError("An exception occurred while executing request: {ex}", context.Exception);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            if (exception != null)
            {
                var details = new ValidationProblemDetails(exception.Errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                };
                context.Result = new BadRequestObjectResult(details);
            }

            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Warning);
        }

        private static void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Fatal);
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = context.Exception as BadRequestException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "An error occurred while processing your request.",
                Detail = exception?.Message
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Error);
        }

        private void HandleForbiddenException(ExceptionContext context)
        {
            var exception = context.Exception as ForbiddenException;

            var details = new ProblemDetails()
            {
                Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
                Title = "Forbidden",
                Detail = exception?.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Error);
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception?.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Error);
        }

        private void HandleUnauthorizedException(ExceptionContext context)
        {
            var exception = context.Exception as UnauthorizedException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Unauthorized",
                Detail = exception?.Message
            };

            context.Result = new UnauthorizedObjectResult(details);
            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Warning);
        }

        private static void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            Console.WriteLine(context.Exception.Message);
            context.ExceptionHandled = true;
            CaptureSentryException(context.Exception, SentryLevel.Fatal);
        }

        private static void CaptureSentryException(Exception exception, SentryLevel level)
        {
            // TODO: Add some logic to determine if we should capture the exception
            if (true)
            {
                SentrySdk.CaptureException(exception, (scope) => { scope.Level = level; });
            }
        }
    }
}
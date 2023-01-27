using Mediator;
using Microsoft.Extensions.Logging;

namespace Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public class GetAllFaqsQueryHandler : IRequestHandler<GetAllFaqsQuery, List<GetFaqDto>>
    {
        private readonly ILogger<GetAllFaqsQueryHandler> _logger;

        public GetAllFaqsQueryHandler(ILogger<GetAllFaqsQueryHandler> logger)
        {
            _logger = logger;
        }

        public ValueTask<List<GetFaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            var faqs = new List<GetFaqDto>
            {
                new GetFaqDto
                {
                    Question = "How to fix CORS Issue?",
                    Answer = "Register services.AddCors() method in the dependecy injection class."
                }
            };

            _logger.LogInformation("retrieved FAQS..");

            return ValueTask.FromResult(faqs);
        }
    }
}
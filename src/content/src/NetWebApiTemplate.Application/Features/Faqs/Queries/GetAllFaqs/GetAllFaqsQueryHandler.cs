using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetWebApiTemplate.Application.Shared.Extensions;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Application.Shared.Models;

namespace NetWebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed class GetAllFaqsQueryHandler : IRequestHandler<GetAllFaqsQuery, PaginatedList<GetFaqDto>>
    {
        private readonly INetWebApiTemplateDbContext _dbContext;
        private readonly ILogger<GetAllFaqsQueryHandler> _logger;

        public GetAllFaqsQueryHandler(INetWebApiTemplateDbContext dbContext, ILogger<GetAllFaqsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async ValueTask<PaginatedList<GetFaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            var faqQuery = _dbContext.Faqs.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                faqQuery = faqQuery.Where(q => q.Question.Contains(request.SearchTerm.Trim()));
            }

            var faqs = await faqQuery
                .AsNoTracking()
                .Select(f => new GetFaqDto
                {
                    Question = f.Question,
                    Answer = f.Answer
                })
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            _logger.LogInformation("retrieved FAQS.");

            return faqs;
        }
    }
}
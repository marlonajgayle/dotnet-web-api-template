using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Net7WebApiTemplate.Application.Shared.Extensions;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Application.Shared.Models;

namespace Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed class GetAllFaqsQueryHandler : IRequestHandler<GetAllFaqsQuery, PaginatedList<GetFaqDto>>
    {
        private readonly INet7WebApiTemplateDbContext _dbContext;
        private readonly ILogger<GetAllFaqsQueryHandler> _logger;

        public GetAllFaqsQueryHandler(INet7WebApiTemplateDbContext dbContext, ILogger<GetAllFaqsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async ValueTask<PaginatedList<GetFaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            var faqQuery =  _dbContext.Faqs.AsQueryable();

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
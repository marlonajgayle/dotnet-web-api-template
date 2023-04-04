using Mediator;
using NetWebApiTemplate.Application.Shared.Models;

namespace NetWebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed record GetAllFaqsQuery : IRequest<PaginatedList<GetFaqDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
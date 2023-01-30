using Mediator;
using Net7WebApiTemplate.Application.Shared.Models;

namespace Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed record GetAllFaqsQuery : IRequest<PaginatedList<GetFaqDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Offset { get; set; }
        public int Limit { get; set; }  
    }
}
using MediatR;

namespace Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public class GetAllFaqsQuery : IRequest<IList<GetFaqDto>>
    {
    }
}
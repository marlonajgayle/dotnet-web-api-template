using MediatR;

namespace Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs
{
    public class GetAllFaqsQueryHandler : IRequestHandler<GetAllFaqsQuery, IList<GetFaqDto>>
    {
        public  async Task<IList<GetFaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            var faqs = new List<GetFaqDto>
            {
                new GetFaqDto
                {
                    Question = "How to fix CORS Issue?",
                    Answer = "Register services.AddCors() method in the dependecy injection class."
                }
            };

            return faqs;
        }
    }
}
namespace Net7WebApiTemplate.Api.Endpoints.Faqs
{
    public class FaqRequest : PaginationRequest
    {
        public string SearchTerm { get; set; } = string.Empty;

    }
}
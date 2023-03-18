namespace Net7WebApiTemplate.Api.Endpoints
{
    public class PaginationRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public PaginationRequest()
        {
            Offset = 1;
            Limit = 25;
        }

        public PaginationRequest(int offset, int limit)
        {
            Offset = offset < 1 ? 1 : offset;
            Limit = limit > 50 ? 50 : limit;
        }
    }
}
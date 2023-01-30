namespace Net7WebApiTemplate.Api.Endpoints
{
    public class PaginationRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public PaginationRequest()
        {
            Offset = 0;
            Limit = 0;
        }

        public PaginationRequest(int offset, int limit)
        { 
            Offset = offset;
            Limit = limit;
        }
    }
}
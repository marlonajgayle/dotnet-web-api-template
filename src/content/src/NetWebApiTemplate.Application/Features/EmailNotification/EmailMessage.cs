namespace NetWebApiTemplate.Application.Features.EmailNotification
{
    public class EmailMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public object Model { get; set; } = new object();
    }
}
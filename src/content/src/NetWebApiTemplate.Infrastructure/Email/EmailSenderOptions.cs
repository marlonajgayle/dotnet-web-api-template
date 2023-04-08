namespace NetWebApiTemplate.Infrastructure.Email
{
    public class EmailSenderOptions
    {
        public string FromEmail { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; }
    }
}
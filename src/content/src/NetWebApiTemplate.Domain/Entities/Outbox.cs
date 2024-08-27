namespace NetWebApiTemplate.Domain.Entities
{
    public class Outbox
    {
        public Guid Id { get; set; }
        public string MessageType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
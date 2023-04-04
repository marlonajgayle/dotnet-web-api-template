namespace NetWebApiTemplate.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string JwtId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
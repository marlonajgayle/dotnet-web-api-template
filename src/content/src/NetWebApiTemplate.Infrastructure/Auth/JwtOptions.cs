namespace NetWebApiTemplate.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;
        public TimeSpan Expiration { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; }
        public string Audience { get; set; } = string.Empty;
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
    }
}
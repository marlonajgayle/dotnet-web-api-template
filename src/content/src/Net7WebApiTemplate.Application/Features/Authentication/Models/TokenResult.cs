namespace Net7WebApiTemplate.Application.Features.Authentication.Models
{
    public class TokenResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
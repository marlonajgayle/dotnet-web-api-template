namespace NetWebApiTemplate.Application.Features.HealthChecks
{
    public class HealthCheckItem
    {
        public string Status { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }
}
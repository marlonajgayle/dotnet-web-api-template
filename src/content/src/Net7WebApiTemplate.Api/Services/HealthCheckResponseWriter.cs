using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net7WebApiTemplate.Application.Features.HealthChecks;
using Newtonsoft.Json;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Net7WebApiTemplate.Api.Services
{
    public static class HealthCheckResponseWriter
    {
        public static async Task WriterHealthCheckResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new HealthCheckResponse()
            {
                OverallStatus = report.Status.ToString(),
                TotalDuration = report.TotalDuration.TotalSeconds.ToString("0:0.00"),
                HealthChecks = report.Entries.Select(x => new HealthCheckItem
                {
                    Status = x.Value.Status.ToString(),
                    Component = x.Key,
                    Description = x.Value.Description ?? "",
                    Duration = x.Value.Duration.TotalSeconds.ToString("0:0.00")
                })
            };

            await httpContext.Response.WriteAsync(text: JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}

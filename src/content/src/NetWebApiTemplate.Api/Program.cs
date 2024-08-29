using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NetWebApiTemplate.Api.Filters;
using NetWebApiTemplate.Api.Services;
using NetWebApiTemplate.Api.Swagger;
using NetWebApiTemplate.Application;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Infrastructure;
using NetWebApiTemplate.Persistence;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// loading appsettings.json based on environment configurations
var env = builder.Environment;

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

// apply user secrets when running on local environment
if (env.EnvironmentName == "Local")
{
    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
    if (appAssembly != null)
    {
        builder.Configuration.AddUserSecrets(appAssembly, optional: true);
    }
}

// add environment variables
builder.Configuration.AddEnvironmentVariables();

// apply commandline arguments
if (args != null)
{
    builder.Configuration.AddCommandLine(args);
}

#if Sentry
// Configure Sentry.io APM
builder.WebHost.UseSentry(options =>
{
    // Performance monitoring
    // Set sample rate: capture % of transactions
    options.TracesSampleRate = env.IsProduction() ? 0.2 : 1.0;
});
#endif

//-- Add services to the container.
// needed to load configurations from appsettings.json
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

// load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

// inject counter and rules stores
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// client IP resolvers use it
builder.Services.AddHttpContextAccessor();

// Register Current User Service
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Add library project reference
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddPersistence(builder.Configuration, builder.Environment);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var swaggerDocOptions = new SwaggerDocOptions();
builder.Configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<SwaggerGenOptions>()
    .Configure<IApiVersionDescriptionProvider>((swagger, service) =>
    {
        foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
        {
            swagger.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = swaggerDocOptions.Title,
                Version = description.ApiVersion.ToString(),
                Description = swaggerDocOptions.Description,
                TermsOfService = new Uri("https://github.com"),
                Contact = new OpenApiContact
                {
                    Name = swaggerDocOptions.Organization,
                    Email = swaggerDocOptions.Email
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://github.com/")
                }
            });
        }

        var security = new Dictionary<string, IEnumerable<string>>
        {
            {"Bearer", Array.Empty<string>()}
        };

        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        swagger.OperationFilter<AuthorizeCheckOperationFilter>();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        swagger.IncludeXmlComments(xmlPath);

    });

// Register API Exception Filter
builder.Services.AddControllersWithViews(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());

// Configure HTTP Strict Transport Security Protocol (HSTS)
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(1);
});

// Register and configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        options =>
        {
            options.WithOrigins(builder.Configuration.GetSection("Cors:Origins")
            .Get<string[]>() ?? Array.Empty<string>())
            .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE")
            .AllowCredentials();
        });

    options.AddPolicy(name: "CorsPolicyAny",
        options =>
        {
            options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

// Register and configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Register and configure APi versioning explorer
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

//-- Configure the HTTP request pipeline
var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local") || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Enable HTTP Strict Transport Security Protocol (HSTS)
    app.UseHsts();
}

// Enable NWebSec Security Response Headers
app.UseXContentTypeOptions();
app.UseXfo(options => options.SameOrigin());
app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseReferrerPolicy(options => options.NoReferrerWhenDowngrade());

// Feature-Policy security Header
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Feature-Policy", "geolocation 'none'; midi 'none';");
    await next.Invoke();
});

// Apply pending database migrations
if (builder.Configuration.GetValue<bool>("UseDatabaseInitializer"))
{
    app.UseInitializeDatabase();
}

// Enable IP Rate Limiting Middleware
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

#if Sentry
// Enable automatic tracing
app.UseSentryTracing();
#endif


app.UseCors(builder.Configuration.GetValue<string>("Cors:Policy"));

app.UseAuthorization();

// Configure custom healthcheck endpoint
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = HealthCheckResponseWriter.WriterHealthCheckResponse,
    AllowCachingResponses = false
});

app.MapControllers();

app.Run();

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Service;
using Service.Configuration;
using Service.Health;
using Service.Settings;
using Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient()
    .AddHttpClient<ApiConfigurationProvider>(builder.Configuration);

builder.Configuration.AddApiSettings(builder.Services.BuildServiceProvider());

builder.Services.AddHealthChecks()
    .AddCheck<ApiConfigurationHealthCheck>(nameof(ApiConfigurationHealthCheck))
    .AddWorkingSetHealthCheck(1024)
    .AddDiskStorageHealthCheck(opt => {
        opt.AddDrive("c:\\",1024*20);
        opt.AddDrive("f:\\",1024*20);
    });

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(10);
    opt.SetApiMaxActiveRequests(1);
    opt.AddHealthCheckEndpoint("default api", "/health");
}).AddInMemoryStorage();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecksUI(opt =>
{
    opt.UIPath= "/health-ui";
});

var group = app.MapGroup("/service").WithTags("Service");

group.MapPost("setting/reload", async (IConfiguration config, CancellationToken token = default) =>
{
    if (config is not IConfigurationRoot root) { return; }

    foreach (var provider in root.Providers)
    {
        if (provider is not ApiConfigurationProvider apiProvider) { continue; }

        await apiProvider.Reload(token);
    }
}).WithName("ReloadSettings").WithOpenApi();

app.Run();
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

builder.Services.AddHealthChecks().AddCheck<ApiConfigurationHealthCheck>("basic", tags: ["tag"]);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
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
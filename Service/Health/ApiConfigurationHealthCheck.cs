using Microsoft.Extensions.Diagnostics.HealthChecks;
using Service.Configuration;
using System.Reflection;

namespace Service.Health
{
    public class ApiConfigurationHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public ApiConfigurationHealthCheck(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (this._configuration is not IConfigurationRoot root) { return Task.FromResult(HealthCheckResult.Unhealthy($"Configuration is not of type {nameof(IConfigurationRoot)}")); }

            if (root.Providers.FirstOrDefault(x => x is ApiConfigurationProvider) is not ApiConfigurationProvider apiProvider) { return Task.FromResult(HealthCheckResult.Unhealthy($"No {nameof(ApiConfigurationProvider)} registered")); }

            var dict = new Dictionary<string, object>
            {
                ["apiConfiguration.loadTime"] = apiProvider.LoadTime,
                ["apiConfiguration.version"] = Assembly.GetExecutingAssembly().GetName().Version,
                ["apiConfiguration.data"] = apiProvider.CurrentData,

            };

            return Task.FromResult(HealthCheckResult.Healthy("OK", dict));
        }
    }
}

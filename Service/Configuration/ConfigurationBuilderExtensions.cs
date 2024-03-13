using Service.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Service.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddApiSettings(this IConfigurationBuilder builder, IServiceProvider services, int? reloadSeconds = null, CancellationToken cancellationToken = default)
        => builder.Add(new ApiConfigurationSource()
        {
            Services = services,
            Stream = LoadStreamSync(GetStreamTask(services.GetRequiredService<IHttpClientFactory>(), cancellationToken), cancellationToken),
            ReloadSeconds = reloadSeconds,
            ReloadStream = GetStreamTask,
            Token = cancellationToken,
        });

        private static Stream LoadStreamSync(Task<string> streamTask, CancellationToken cancellationToken)
        {
            if (streamTask.Wait(TimeSpan.FromMinutes(1), cancellationToken))
            {
                var byteArray = Encoding.UTF8.GetBytes(streamTask.Result);
                return new MemoryStream(byteArray);
            }
            throw new Exception($"Error on load Configuration");
        }

        private static Task<string> GetStreamTask(IHttpClientFactory factory, CancellationToken cancellationToken)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var client = factory.CreateClient<ApiConfigurationProvider>();
            return client.GetStringAsync($"services/settings/{version}", cancellationToken);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Service.Extensions
{
    public static class DIExt
    {
        public static HttpClient CreateClient<TService>(this IHttpClientFactory httpClientFactory) where TService : class
            => httpClientFactory.CreateClient(typeof(TService).Name);

        public static IServiceCollection AddHttpClient<TService>(this IServiceCollection services, IConfiguration configuration) where TService : class
            => services.AddHttpClient(typeof(TService).Name, new Uri(configuration.GetSection("Api").Value), configuration.GetSection("x-api-key").Value);

        private static IServiceCollection AddHttpClient(this IServiceCollection services, string name, Uri baseAddress, string token)
        {
            services.AddHttpClient(name, client =>
               {
                   client.BaseAddress = baseAddress;
                   client.DefaultRequestHeaders.Accept.Clear();
                   client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                   client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
               });
            return services;
        }
    }
}

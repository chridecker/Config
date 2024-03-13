using Microsoft.Extensions.DependencyInjection;
using System.Dynamic;

namespace Service.Extensions
{
    public static class DIExt
    {
        public const string SCHEME = "Bearer";
        public const string API_KEY = "Api";
        public const string API_TOKEN_KEY = "api-token";


        public static HttpClient CreateClient<TService>(this IHttpClientFactory httpClientFactory) where TService : class
            => httpClientFactory.CreateClient(typeof(TService).Name);

        public static IServiceCollection AddHttpClient<TService>(this IServiceCollection services, IConfiguration configuration) where TService : class
            => services.AddHttpClient(typeof(TService).Name, new Uri(configuration.GetSection(API_KEY).Value), configuration.GetSection(API_TOKEN_KEY).Value);

        private static IServiceCollection AddHttpClient(this IServiceCollection services, string name, Uri baseAddress, string token)
        {
            services.AddHttpClient(name, client =>
               {
                   client.BaseAddress = baseAddress;
                   client.DefaultRequestHeaders.Accept.Clear();
                   client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                   client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(SCHEME, token);
               });
            return services;
        }
    }
}

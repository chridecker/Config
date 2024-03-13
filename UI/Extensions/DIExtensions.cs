using Microsoft.Extensions.Configuration;

namespace UI.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUI(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}

using DataAccess;
using DionySys.UI.Shared.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UI.Services;

namespace UI.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("Main"));
            });

            services.AddQuickGridEntityFrameworkAdapter();

            services.AddScoped<NavigationHandler>();

            services.AddScoped<IUIHandler, UIHandler>();

            return services;
        }
    }
}

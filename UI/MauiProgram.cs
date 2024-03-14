using Blazored.Modal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using UI.Extensions;

namespace UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Configuration.AddAppsettingConfiguration();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddUI(builder.Configuration);

            builder.Services.AddBlazoredModal();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void AddAppsettingConfiguration(this IConfigurationBuilder configuration)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(typeof(MauiProgram).Namespace + ".appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            configuration.AddConfiguration(config);
        }
    }
}

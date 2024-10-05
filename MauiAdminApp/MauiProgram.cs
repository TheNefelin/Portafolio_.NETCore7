using MauiAdminApp.Pages;
using MauiAdminApp.Services;
using Microsoft.Extensions.Logging;

namespace MauiAdminApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Inyección de dependencias de HttpClient y AuthService
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://artema.bsite.net/") });
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ApiUrlGrpService>();

            // Registrar LoginPage para que reciba el AuthService inyectado
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MessagesPage>();
            builder.Services.AddTransient<MasterMenuPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

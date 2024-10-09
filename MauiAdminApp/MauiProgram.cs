using CommunityToolkit.Maui;
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
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Inyección de dependencias de HttpClient y Services
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://artema.bsite.net/") });
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PasswordManagerService>();
            builder.Services.AddSingleton<ApiUrlGrpService>();

            // Registrar Pages para que reciba el Services inyectado
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PasswordManagerPage>();
            builder.Services.AddTransient<PasswordManagerFormPage>();

            builder.Services.AddTransient<MessagesPage>();
            builder.Services.AddTransient<ChatPage>();

            builder.Services.AddTransient<UrlGrpListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

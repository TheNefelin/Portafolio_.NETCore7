using MauiAdminApp.Pages;
using MauiAdminApp.Services;

namespace MauiAdminApp
{
    public partial class App : Application
    {
        public static IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Forzar el tema oscuro
            Application.Current.UserAppTheme = AppTheme.Dark;

            // Almacenar el servicio inyectado y el AuthService
            _serviceProvider = serviceProvider;

            // Asignar una página de carga inicial mientras se valida el token
            MainPage = new LoadingPage();
            _ = InitializeApp();
        }

        private async Task InitializeApp()
        {
            // Verificar si el usuario está autenticado
            var authService = _serviceProvider.GetService<AuthService>();
            bool isAuthenticated = await authService.CheckIfUserIsAuthenticated();

            // Si el usuario está autenticado, mostrar la Shell (menú)
            if (isAuthenticated)
            {
                MainPage = new AppShell(); // Menú de la aplicación
            }
            else
            {
                // Usamos el `serviceProvider` para obtener la instancia de LoginPage directamente
                MainPage = new NavigationPage(_serviceProvider.GetService<LoginPage>());
            }
        }

    }
}

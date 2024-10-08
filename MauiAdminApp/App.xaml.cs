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

            // Almacenar el servicio inyectado y el AuthService
            _serviceProvider = serviceProvider;

            // Asignar una página de carga inicial mientras se valida el token
            MainPage = new LoadingPage();
            _ = InitializeApp();
        }

        private async Task InitializeApp()
        {
            // Verificar si el usuario está autenticado
            bool isAuthenticated = await CheckIfUserIsAuthenticated();

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

        private async Task<bool> CheckIfUserIsAuthenticated()
        {
            try
            {
                Console.WriteLine("Verificando si el usuario tiene un token almacenado...");

                var jwtToken = await SecureStorage.GetAsync("jwt_token");

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    Console.WriteLine("Token encontrado. Validando con el servidor...");
                    var authService = new AuthService(new HttpClient { BaseAddress = new Uri("https://artema.bsite.net/") });
                    return await authService.ValidateToken(jwtToken);
                }

                Console.WriteLine("No se encontró un token.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar la autenticación: {ex.Message}");
                return false;
            }
        }
    }
}

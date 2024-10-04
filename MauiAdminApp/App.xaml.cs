using MauiAdminApp.Pages;

namespace MauiAdminApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Verificar si el usuario está autenticado
            bool isAuthenticated = CheckIfUserIsAuthenticated();

            // Si el usuario está autenticado, mostrar la Shell (menú)
            if (isAuthenticated)
            {
                MainPage = new AppShell(); // Menú de la aplicación
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage()); // Página de Login
            }
        }

        private bool CheckIfUserIsAuthenticated()
        {
            // Aquí iría la lógica para verificar si el usuario está autenticado
            // Por ejemplo, podrías revisar si existe un token o credencial almacenada
            return false; // Por defecto el usuario no está autenticado
        }
    }
}

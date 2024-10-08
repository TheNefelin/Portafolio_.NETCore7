using MauiAdminApp.Services;

namespace MauiAdminApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            AuthService.RemoveToken(); // Asegúrate de usar await

            // Navegar a la página de inicio de sesión
            await Shell.Current.GoToAsync("//LoginPage"); // Usa la ruta definida
        }
    }
}

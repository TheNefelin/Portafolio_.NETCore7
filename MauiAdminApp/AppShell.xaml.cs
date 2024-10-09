using MauiAdminApp.Pages;
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
            await Shell.Current.GoToAsync("//LoadingPage");

            AuthService.RemoveToken(); // Asegúrate de usar await

            // Remover el botón de Logout
            var toolbarItem = this.ToolbarItems.FirstOrDefault(item => item.Text == "Logout");
            if (toolbarItem != null)
            {
                this.ToolbarItems.Remove(toolbarItem);
            }

            // Navegar a la página de inicio de sesión
            await Shell.Current.GoToAsync("//LoginPage"); // Usa la ruta definida
        }

    }
}

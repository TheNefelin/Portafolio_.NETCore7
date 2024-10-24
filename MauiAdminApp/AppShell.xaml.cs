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
           await PerformLogout();
        }

        public async Task PerformLogout()
        {
            await Shell.Current.GoToAsync("//LoadingPage");

            // remove all secrets
            SecureStorage.RemoveAll();
            //await SecureStorage.SetAsync("jwt_token", string.Empty); // O puedes usar SecureStorage.RemoveAsync("jwt_token");
            //await SecureStorage.SetAsync("sql_token", string.Empty); // Si deseas limpiar también este token

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

using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingresa un correo electrónico y contraseña", "OK");
            return;
        }

        // Lógica para autenticación aquí (ejemplo: llamada a un servicio de API)
        //bool isAuthenticated = await Authenticate(UsernameEntry.Text, PasswordEntry.Text);
        bool isAuthenticated = UsernameEntry.Text.Equals("") || PasswordEntry.Text.Equals("") ? false : true;

        if (isAuthenticated)
        {
            // Cambiar la página principal a AppShell después de la autenticación exitosa
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}
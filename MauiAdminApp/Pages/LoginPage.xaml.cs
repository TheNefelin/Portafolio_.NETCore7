using MauiAdminApp.Services;

namespace MauiAdminApp;

public partial class LoginPage : ContentPage
{
    private AuthService _authService;

    public LoginPage()
	{
		InitializeComponent();
        _authService = new AuthService();
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

        var loginResponse = await _authService.Login(email, password);

        if (loginResponse != null)
        {
            // Redirigir a la pantalla principal de la aplicación
            //Application.Current.MainPage = new NavigationPage(new AppShell());
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }

}
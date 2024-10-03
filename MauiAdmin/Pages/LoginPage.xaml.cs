using MauiAdmin.Services;

namespace MauiAdmin.Pages;

public partial class LoginPage : ContentPage
{
    private AuthService _authService;

    public LoginPage()
	{
		InitializeComponent();
        _authService = new AuthService();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
	{
        string email = emailEntry.Text;
        string password = passwordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingresa un correo electrónico y contraseña", "OK");
            return;
        }

        // Llamar al servicio de autenticación
        var loginResponse = await _authService.Login(email, password);

        if (loginResponse != null)
        {
            // Si la autenticación fue exitosa, redirigir a la página principal
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Email o contraseña incorrectos", "OK");
        }
    }
}
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
            await DisplayAlert("Error", "Por favor ingresa un correo electr�nico y contrase�a", "OK");
            return;
        }

        // Llamar al servicio de autenticaci�n
        var loginResponse = await _authService.Login(email, password);

        if (loginResponse != null)
        {
            // Si la autenticaci�n fue exitosa, redirigir a la p�gina principal
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Email o contrase�a incorrectos", "OK");
        }
    }
}
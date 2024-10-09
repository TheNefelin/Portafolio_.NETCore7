using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoadingPage();

        string email = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingresa un correo electrónico y contraseña", "OK");
            return;
        }

        // Lógica para autenticación aquí (ejemplo: llamada a un servicio de API)
        ResponseApiDTO<LoggedinDTO> responseApi = await _authService.Login(UsernameEntry.Text, PasswordEntry.Text);

        if (responseApi != null)
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
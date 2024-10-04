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
        string email = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingresa un correo electrónico y contraseña", "OK");
            return;
        }

        // Lógica para autenticación aquí (ejemplo: llamada a un servicio de API)
        ResponseApiDTO<LoggedinDTO> responseApi = await _authService.Login(UsernameEntry.Text, PasswordEntry.Text);
        //bool isAuthenticated = UsernameEntry.Text.Equals("") || PasswordEntry.Text.Equals("") ? false : true;

        if (responseApi != null)
        {
            await DisplayAlert("Token", responseApi.Data.ApiToken, "OK");

            // Cambiar la página principal a AppShell después de la autenticación exitosa
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}
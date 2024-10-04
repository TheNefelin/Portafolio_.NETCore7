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
            await DisplayAlert("Error", "Por favor ingresa un correo electr�nico y contrase�a", "OK");
            return;
        }

        // L�gica para autenticaci�n aqu� (ejemplo: llamada a un servicio de API)
        ResponseApiDTO<LoggedinDTO> responseApi = await _authService.Login(UsernameEntry.Text, PasswordEntry.Text);
        //bool isAuthenticated = UsernameEntry.Text.Equals("") || PasswordEntry.Text.Equals("") ? false : true;

        if (responseApi != null)
        {
            await DisplayAlert("Token", responseApi.Data.ApiToken, "OK");

            // Cambiar la p�gina principal a AppShell despu�s de la autenticaci�n exitosa
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }
}
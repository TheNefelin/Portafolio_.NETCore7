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
            await DisplayAlert("Error", "Por favor ingresa un correo electr�nico y contrase�a", "OK");
            return;
        }

        // L�gica para autenticaci�n aqu� (ejemplo: llamada a un servicio de API)
        ResponseApiDTO<LoggedinDTO> responseApi = await _authService.Login(UsernameEntry.Text, PasswordEntry.Text);

        if (responseApi != null)
        {

            // Cambiar la p�gina principal a AppShell despu�s de la autenticaci�n exitosa
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }
}
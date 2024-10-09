using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerPage : ContentPage
{
	private readonly PasswordManagerService _passwordManagerService;
    public PasswordManagerPage(PasswordManagerService passwordManagerService)
	{
		InitializeComponent();
		_passwordManagerService = passwordManagerService;
    }

	private async void OnDownloadData(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new PasswordPromptPage());
        await Navigation.PushModalAsync(new PasswordPromptPage());

        // Mostrar un cuadro de di�logo solicitando la contrase�a
        string password = await DisplayPromptAsync("Descargar Datos", "Por favor, ingrese la contrase�a para continuar:",
                                                   maxLength: 16, keyboard: Keyboard.Text,
                                                   placeholder: "Contrase�a", accept: "Aceptar", cancel: "Cancelar");

        if (!string.IsNullOrEmpty(password))
        {
            // Aqu� puedes agregar l�gica para validar la contrase�a ingresada
            bool isPasswordValid = true;

            if (isPasswordValid)
            {
                await DisplayAlert("�xito", "Descarga de datos completada.", "OK");
                // L�gica para descargar los datos
            }
            else
            {
                await DisplayAlert("Error", "Contrase�a incorrecta.", "OK");
            }
        }
    }

    private async void OnCreateSecret(object sender, EventArgs e)
    {
		await Navigation.PushAsync(App._serviceProvider.GetService<PasswordManagerFormPage>());
	}
}
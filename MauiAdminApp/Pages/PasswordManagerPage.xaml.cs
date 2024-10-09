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

        // Mostrar un cuadro de diálogo solicitando la contraseña
        string password = await DisplayPromptAsync("Descargar Datos", "Por favor, ingrese la contraseña para continuar:",
                                                   maxLength: 16, keyboard: Keyboard.Text,
                                                   placeholder: "Contraseña", accept: "Aceptar", cancel: "Cancelar");

        if (!string.IsNullOrEmpty(password))
        {
            // Aquí puedes agregar lógica para validar la contraseña ingresada
            bool isPasswordValid = true;

            if (isPasswordValid)
            {
                await DisplayAlert("Éxito", "Descarga de datos completada.", "OK");
                // Lógica para descargar los datos
            }
            else
            {
                await DisplayAlert("Error", "Contraseña incorrecta.", "OK");
            }
        }
    }

    private async void OnCreateSecret(object sender, EventArgs e)
    {
		await Navigation.PushAsync(App._serviceProvider.GetService<PasswordManagerFormPage>());
	}
}
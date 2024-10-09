namespace MauiAdminApp.Pages;

public partial class PasswordPromptPage : ContentPage
{
	public PasswordPromptPage()
	{
		InitializeComponent();
	}

    // Evento cuando se hace clic en el bot�n "Aceptar"
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        // Validar si la contrase�a tiene al menos 4 caracteres
        if (string.IsNullOrEmpty(password) || password.Length < 4)
        {
            ErrorLabel.Text = "La contrase�a debe tener al menos 4 caracteres.";
            ErrorLabel.IsVisible = true;
        }
        else
        {
            // Ocultar la etiqueta de error si la validaci�n es correcta
            ErrorLabel.IsVisible = false;

            // Aqu� puedes continuar con la l�gica de descarga de datos o lo que necesites
            await DisplayAlert("Contrase�a v�lida", "Procesando...", "OK");

            // Cerrar la p�gina
            await Navigation.PopAsync();
        }
    }

    // Evento cuando se hace clic en el bot�n "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Simplemente regresa a la p�gina anterior sin realizar acci�n
        //await Navigation.PopAsync();
        await Navigation.PopModalAsync();
    }
}
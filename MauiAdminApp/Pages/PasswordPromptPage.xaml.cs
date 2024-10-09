namespace MauiAdminApp.Pages;

public partial class PasswordPromptPage : ContentPage
{
	public PasswordPromptPage()
	{
		InitializeComponent();
	}

    // Evento cuando se hace clic en el botón "Aceptar"
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        // Validar si la contraseña tiene al menos 4 caracteres
        if (string.IsNullOrEmpty(password) || password.Length < 4)
        {
            ErrorLabel.Text = "La contraseña debe tener al menos 4 caracteres.";
            ErrorLabel.IsVisible = true;
        }
        else
        {
            // Ocultar la etiqueta de error si la validación es correcta
            ErrorLabel.IsVisible = false;

            // Aquí puedes continuar con la lógica de descarga de datos o lo que necesites
            await DisplayAlert("Contraseña válida", "Procesando...", "OK");

            // Cerrar la página
            await Navigation.PopAsync();
        }
    }

    // Evento cuando se hace clic en el botón "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Simplemente regresa a la página anterior sin realizar acción
        //await Navigation.PopAsync();
        await Navigation.PopModalAsync();
    }
}
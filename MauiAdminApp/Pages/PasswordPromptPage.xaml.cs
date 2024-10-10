namespace MauiAdminApp.Pages;

public partial class PasswordPromptPage : ContentPage
{
    private TaskCompletionSource<string> _taskCompletionSource;

    public PasswordPromptPage()
	{
		InitializeComponent();
	}

    // M�todo para mostrar el modal y esperar la contrase�a
    public Task<string> GetPasswordAsync()
    {
        _taskCompletionSource = new TaskCompletionSource<string>();
        return _taskCompletionSource.Task;
    }

    // Evento cuando se hace clic en el bot�n "Aceptar"
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        // Validar si la contrase�a tiene al menos 4 caracteres
        if (string.IsNullOrEmpty(password) || password.Length < 6)
        {
            ErrorLabel.Text = "La contrase�a debe tener al menos 6 caracteres.";
            ErrorLabel.IsVisible = true;
        }
        else
        {
            // Ocultar la etiqueta de error si la validaci�n es correcta
            ErrorLabel.IsVisible = false;

            // Devolver la contrase�a
            _taskCompletionSource.SetResult(password);

            // Cerrar la p�gina
            await Navigation.PopModalAsync();
        }
    }

    // Evento cuando se hace clic en el bot�n "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Devolver null si se cancela
        _taskCompletionSource.SetResult(null);

        // Simplemente regresa a la p�gina anterior sin realizar acci�n
        await Navigation.PopModalAsync();
    }
}
namespace MauiAdminApp.Pages;

public partial class PasswordPromptPage : ContentPage
{
    private TaskCompletionSource<string> _taskCompletionSource;

    public PasswordPromptPage()
	{
		InitializeComponent();
	}

    // Método para mostrar el modal y esperar la contraseña
    public Task<string> GetPasswordAsync()
    {
        _taskCompletionSource = new TaskCompletionSource<string>();
        return _taskCompletionSource.Task;
    }

    // Evento cuando se hace clic en el botón "Aceptar"
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        // Validar si la contraseña tiene al menos 4 caracteres
        if (string.IsNullOrEmpty(password) || password.Length < 6)
        {
            ErrorLabel.Text = "La contraseña debe tener al menos 6 caracteres.";
            ErrorLabel.IsVisible = true;
        }
        else
        {
            // Ocultar la etiqueta de error si la validación es correcta
            ErrorLabel.IsVisible = false;

            // Devolver la contraseña
            _taskCompletionSource.SetResult(password);

            // Cerrar la página
            await Navigation.PopModalAsync();
        }
    }

    // Evento cuando se hace clic en el botón "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Devolver null si se cancela
        _taskCompletionSource.SetResult(null);

        // Simplemente regresa a la página anterior sin realizar acción
        await Navigation.PopModalAsync();
    }
}
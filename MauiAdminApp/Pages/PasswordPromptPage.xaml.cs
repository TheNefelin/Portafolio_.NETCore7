using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordPromptPage : ContentPage
{
    private readonly PasswordManagerService _passwordManagerService;
    private TaskCompletionSource<(string, CoreIVDTO)> _taskCompletionSource;

    public PasswordPromptPage(PasswordManagerService passwordManagerService)
	{
		InitializeComponent();
        _passwordManagerService = passwordManagerService;
        _taskCompletionSource = new TaskCompletionSource<(string, CoreIVDTO)>();
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
            return;
        }

        // Ocultar la etiqueta de error si la validación es correcta
        ErrorLabel.IsVisible = false;

        var (result, susces) = await _passwordManagerService.Login(password);

        if (!susces)
        {
            await DisplayAlert($"{result.StatusCode}", $"{result.Message}", "Ok");
            return;
        }

        _taskCompletionSource.SetResult((password, result.Data));
        await Navigation.PopModalAsync();
    }

    // Evento cuando se hace clic en el botón "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _taskCompletionSource.SetResult((null, new CoreIVDTO()));
        await Navigation.PopModalAsync();
    }

    // Método para mostrar el modal y esperar la contraseña
    public Task<(string, CoreIVDTO)> GetPasswordAsync()
    {
        return _taskCompletionSource.Task;
    }
}
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

    // Evento cuando se hace clic en el bot�n "Aceptar"
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        // Validar si la contrase�a tiene al menos 4 caracteres
        if (string.IsNullOrEmpty(password) || password.Length < 6)
        {
            ErrorLabel.Text = "La contrase�a debe tener al menos 6 caracteres.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Ocultar la etiqueta de error si la validaci�n es correcta
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

    // Evento cuando se hace clic en el bot�n "Cancelar"
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _taskCompletionSource.SetResult((null, new CoreIVDTO()));
        await Navigation.PopModalAsync();
    }

    // M�todo para mostrar el modal y esperar la contrase�a
    public Task<(string, CoreIVDTO)> GetPasswordAsync()
    {
        return _taskCompletionSource.Task;
    }
}
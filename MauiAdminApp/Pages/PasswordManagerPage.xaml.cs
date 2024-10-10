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
      
    }

    private async void OnDecryptData(object sender, EventArgs e)
    {
        // Mostrar la página modal y esperar el resultado
        var passwordPrompt = new PasswordPromptPage();
        await Navigation.PushModalAsync(passwordPrompt);
        var passwordResult = await passwordPrompt.GetPasswordAsync();

        if (!string.IsNullOrEmpty(passwordResult))
        {
            await DisplayAlert("OK", passwordResult, "OK");
        }
    }

    private async void OnCreateSecret(object sender, EventArgs e)
    {
		await Navigation.PushAsync(App._serviceProvider.GetService<PasswordManagerFormPage>());
	}
}
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerPage : ContentPage
{
    public PasswordManagerPage()
	{
		InitializeComponent();
	}

	private async void OnCreateSecret(object sender, EventArgs e)
    {
		await Navigation.PushAsync(App._serviceProvider.GetService<PasswordManagerFormPage>());
	}
}
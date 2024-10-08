using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerFormPage : ContentPage
{
    private readonly PasswordManagerService _passwordManagerService;

    public PasswordManagerFormPage(PasswordManagerService passwordManagerService)
	{
		InitializeComponent();

		_passwordManagerService = passwordManagerService;
		LoadPage();
	}

	private async void LoadPage()
	{
		LoggedinDTO loggedinDTO = await AuthService.GetUser();

        IdEntry.Text = "0";
		IdUserEntry.Text = loggedinDTO.Id;
    }

	private async void OnSave(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
	}
}
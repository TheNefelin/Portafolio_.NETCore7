using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerFormPage : ContentPage
{
	public PasswordManagerFormPage()
	{
		InitializeComponent();
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
using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerPage : ContentPage
{
    //public ObservableCollection<Password> Passwords { get; set; }

    public PasswordManagerPage()
	{
		InitializeComponent();
	}

	private async void OnCreateSecret(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new PasswordManagerFormPage());
	}
}
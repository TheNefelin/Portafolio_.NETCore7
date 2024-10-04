namespace MauiAdminApp.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();

        Content = new StackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new ActivityIndicator { IsRunning = true },
                //new Label { Text = "Cargando..." }
            }
        };
    }
}
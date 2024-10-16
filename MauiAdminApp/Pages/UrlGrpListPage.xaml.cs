using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class UrlGrpListPage : ContentPage
{
    private readonly ApiUrlGrpService _apiUrlGrpService;

    public UrlGrpListPage(ApiUrlGrpService apiUrlGrpService)
	{
		InitializeComponent();
		_apiUrlGrpService = apiUrlGrpService;

        loading.IsVisible = true;
        // habilita el loading
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            // Aquí realizas la operación asíncrona, como cargar datos desde una API
            var urlGrps = await _apiUrlGrpService.GetAll();
            UrlGrpListView.ItemsSource = urlGrps;
        }
        finally
        {
            // deshabilita el loading
            loading.IsVisible = false;
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {

    }

    private async void OnEditClicked(object sender, EventArgs e)
    {

    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {

    }
}
using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class ChatPage : ContentPage
{
    private readonly ApiUrlGrpService _apiUrlGrpService;

    public ChatPage(ApiUrlGrpService apiUrlGrpService)
	{
		InitializeComponent();
        _apiUrlGrpService = apiUrlGrpService;

        LoadUrlGrps();
    }


    private async void LoadUrlGrps()
    {
        var urlGrps = await _apiUrlGrpService.GetAll();
        UrlGrpListView.ItemsSource = urlGrps;
    }

    private async void OnAddUrlGrpClicked(object sender, EventArgs e)
    {

    }

    private async void OnEditUrlGrpClicked(object sender, EventArgs e)
    {
        //var button = (ImageButton)sender;
        //var urlGrp = (UrlGrpDTO)button.CommandParameter; // Obtener el par�metro del bot�n

        //// L�gica para editar
        //await Navigation.PushAsync(new EditUrlGrpPage(urlGrp));
    }

    private async void OnDeleteUrlGrpClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var urlGrp = (UrlGrpDTO)button.CommandParameter; // Obtener el par�metro del bot�n

        // L�gica para eliminar
        var result = await DisplayAlert("Confirmar", "�Est�s seguro de que deseas eliminar este elemento?", "S�", "No");
        if (result)
        {
            // Eliminar el elemento
        }
    }

}
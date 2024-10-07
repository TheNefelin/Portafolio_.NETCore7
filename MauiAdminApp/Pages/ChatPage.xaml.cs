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

        ShowLoading(true);
        LoadUrlGrps();
    }

    private async void LoadUrlGrps()
    {
        // Mostrar modal de carga

        try
        {
            // Aqu� realizas la operaci�n as�ncrona, como cargar datos desde una API
            var urlGrps = await _apiUrlGrpService.GetAll();
            UrlGrpListView.ItemsSource = urlGrps;
        }
        finally
        {
            // Cerrar el modal una vez que la operaci�n ha terminado
            ShowLoading(false);
        }
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

    // M�todo para mostrar/ocultar el indicador de carga
    private void ShowLoading(bool isBusy)
    {
        LoadingIndicator.IsVisible = isBusy;
        LoadingIndicator.IsRunning = isBusy;

        // Opcional: deshabilitar la lista si est� cargando
        UrlGrpListView.IsEnabled = !isBusy;
    }
}
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
            // Aquí realizas la operación asíncrona, como cargar datos desde una API
            var urlGrps = await _apiUrlGrpService.GetAll();
            UrlGrpListView.ItemsSource = urlGrps;
        }
        finally
        {
            // Cerrar el modal una vez que la operación ha terminado
            ShowLoading(false);
        }
    }

    private async void OnAddUrlGrpClicked(object sender, EventArgs e)
    {

    }

    private async void OnEditUrlGrpClicked(object sender, EventArgs e)
    {
        //var button = (ImageButton)sender;
        //var urlGrp = (UrlGrpDTO)button.CommandParameter; // Obtener el parámetro del botón

        //// Lógica para editar
        //await Navigation.PushAsync(new EditUrlGrpPage(urlGrp));
    }

    private async void OnDeleteUrlGrpClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var urlGrp = (UrlGrpDTO)button.CommandParameter; // Obtener el parámetro del botón

        // Lógica para eliminar
        var result = await DisplayAlert("Confirmar", "¿Estás seguro de que deseas eliminar este elemento?", "Sí", "No");
        if (result)
        {
            // Eliminar el elemento
        }
    }

    // Método para mostrar/ocultar el indicador de carga
    private void ShowLoading(bool isBusy)
    {
        LoadingIndicator.IsVisible = isBusy;
        LoadingIndicator.IsRunning = isBusy;

        // Opcional: deshabilitar la lista si está cargando
        UrlGrpListView.IsEnabled = !isBusy;
    }
}
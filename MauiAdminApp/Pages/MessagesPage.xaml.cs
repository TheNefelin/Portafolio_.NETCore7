using ClassLibraryDTOs;
using MauiAdminApp.Services;
using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class MessagesPage : ContentPage
{
    public ObservableCollection<UrlGrpDTO> UrlGroups { get; set; }
    private ApiUrlGrpService _urlGrpService;

    public MessagesPage(ApiUrlGrpService apiUrlGrpService)
    {
		InitializeComponent();
        _urlGrpService = apiUrlGrpService;

        // Inicializar la colección observable
        UrlGroups = new ObservableCollection<UrlGrpDTO>();

        // Asignar el BindingContext para el enlace de datos
        BindingContext = this;

        // Cargar los datos de la API
        LoadUrlGroups();
    }

    private async void LoadUrlGroups()
    {
        try
        {
            // Llamar al servicio para obtener los datos
            var urlGroupsFromApi = await _urlGrpService.GetAll();

            if (urlGroupsFromApi != null)
            {
                // Agregar los datos a la colección observable
                foreach (var urlGroup in urlGroupsFromApi)
                {
                    UrlGroups.Add(urlGroup);
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar errores (ej. mostrar alerta)
            await DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
        }
    }
}
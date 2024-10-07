using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class MasterMenuPage : ContentPage
{
    public ObservableCollection<MasterMenu> MenuItems { get; set; }

    public MasterMenuPage()
	{
		InitializeComponent();

        // Simulaci�n de los elementos del men�
        MenuItems = new ObservableCollection<MasterMenu>
        {
            new MasterMenu { Title = "Url Grps", ImageUrl = "urls.png" },
            new MasterMenu { Title = "Urls", ImageUrl = "urls.png" },
            new MasterMenu { Title = "Tecnologias", ImageUrl = "technologies.png" },
            new MasterMenu { Title = "Lenguajes", ImageUrl = "languages.png" },
            new MasterMenu { Title = "Proyectos", ImageUrl = "projects.png" }
        };
        
        BindingContext = this;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var selectedMenu = e.CurrentSelection[0] as MasterMenu;

            var urlGrpsPage = App._serviceProvider.GetService<UrlGrpListPage>();
            await Navigation.PushAsync(urlGrpsPage);

            // Limpiar la selecci�n
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    public class MasterMenu
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class MasterMenuPage : ContentPage
{
    public ObservableCollection<MasterMenu> MenuItems { get; set; }

    public MasterMenuPage()
	{
		InitializeComponent();

        // Simulación de los elementos del menú
        MenuItems = new ObservableCollection<MasterMenu>
        {
            new MasterMenu { Title = "Proyectos", ImageUrl = "btn01.png" },
            new MasterMenu { Title = "Tecnologias", ImageUrl = "btn02.png" },
            new MasterMenu { Title = "Lenguajes", ImageUrl = "btn03.png" },
            new MasterMenu { Title = "Url Grps", ImageUrl = "btn04.png" },
            new MasterMenu { Title = "Urls", ImageUrl = "btn04.png" },
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

            // Limpiar la selección
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    public class MasterMenu
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
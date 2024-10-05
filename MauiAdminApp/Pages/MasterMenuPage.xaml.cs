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
            new MasterMenu { Title = "Url Grps", ImageUrl = "urls.png" },
            new MasterMenu { Title = "Urls", ImageUrl = "urls.png" },
            new MasterMenu { Title = "Tecnologias", ImageUrl = "technologies.png" },
            new MasterMenu { Title = "Lenguajes", ImageUrl = "languages.png" },
            new MasterMenu { Title = "Proyectos", ImageUrl = "projects.png" }
        };
        
        BindingContext = this;
    }

    public class MasterMenu
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
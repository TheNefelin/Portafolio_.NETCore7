using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Game> Games { get; set; }

    public HomePage()
	{
		InitializeComponent();

        // Simulando datos de juegos
        Games = new ObservableCollection<Game>
        {
            new Game { Name = "Juego 1", Description = "Descripci�n del juego 1", ImageUrl = "biblioteca.webp" },
            new Game { Name = "Juego 2", Description = "Descripci�n del juego 2", ImageUrl = "biblioteca.webp" },
            new Game { Name = "Juego 3", Description = "Descripci�n del juego 3", ImageUrl = "biblioteca.webp" },
            new Game { Name = "Juego 4", Description = "Descripci�n del juego 4", ImageUrl = "biblioteca.webp" },
            new Game { Name = "Juego 5", Description = "Descripci�n del juego 5", ImageUrl = "biblioteca.webp" },
            // Agrega m�s juegos aqu�
        };

        BindingContext = this;
    }

	private async void LoadUrlGrps()
	{
	}
}

public class Game
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
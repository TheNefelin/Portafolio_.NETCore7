using ClassLibraryDTOs;
using MauiAdminApp.Services;
using System.Collections.ObjectModel;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerPage : ContentPage
{
	private readonly PasswordManagerService _passwordManagerService;
    public ObservableCollection<CoreDTO> CoreData { get; set; }
    public Command<CoreDTO> EditCommand { get; }
    public Command<CoreDTO> DeleteCommand { get; }

    public PasswordManagerPage(PasswordManagerService passwordManagerService)
	{
		InitializeComponent();

		_passwordManagerService = passwordManagerService;
        CoreData = new ObservableCollection<CoreDTO>();
        EditCommand = new Command<CoreDTO>(OnEdit);
        DeleteCommand = new Command<CoreDTO>(OnDelete);
    }

	private async void OnDownloadData(object sender, EventArgs e)
    {
        SecretsCollectionView.ItemsSource = null;

        var result = await _passwordManagerService.GetAll();
        var sortData = result.Data.OrderBy(dt => dt.Data01).ToList();

        foreach (var data in sortData)
        {
            CoreData.Add(data);
        }

        SecretsCollectionView.ItemsSource = CoreData;
    }

    private async void OnDecryptData(object sender, EventArgs e)
    {
        // Mostrar la página modal y esperar el resultado
        var passwordPrompt = new PasswordPromptPage();
        await Navigation.PushModalAsync(passwordPrompt);
        var passwordResult = await passwordPrompt.GetPasswordAsync();

        if (!string.IsNullOrEmpty(passwordResult))
        {
            await DisplayAlert("OK", passwordResult, "OK");
        }
    }

    private async void OnCreate(object sender, EventArgs e)
    {
		await Navigation.PushAsync(App._serviceProvider.GetService<PasswordManagerFormPage>());
	}
    private void OnEdit(CoreDTO secret)
    {
        // Lógica para editar el secreto
        DisplayAlert("Edit", "Modificando", "Ok");
    }

    private void OnDelete(CoreDTO secret)
    {
        // Lógica para eliminar el secreto
        DisplayAlert("Delete", "Eliminando", "Ok");
    }
}
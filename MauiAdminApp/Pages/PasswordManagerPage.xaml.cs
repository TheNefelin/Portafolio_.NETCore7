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
        BindingContext = this; // Asegurar que el BindingContext esté asignado a la página
    }

    private async void OnGetAll(object sender, EventArgs e)
    {
        await OnDownloadData();
    }

    private async void OnCreate(object sender, EventArgs e)
    {

        var page = new PasswordManagerFormPage(_passwordManagerService, null);
        await Navigation.PushAsync(page);
        var result = await page.GetCompletionTask();

        loading.IsVisible = true;

        if (result)
        {
            await OnDownloadData(); // Solo ejecutar si la operación fue completada (por ejemplo, después de guardar)
        }

        loading.IsVisible = false;
    }

    private async void OnEdit(CoreDTO secret)
    {
        var page = new PasswordManagerFormPage(_passwordManagerService, secret);
        await Navigation.PushAsync(page);
        var result = await page.GetCompletionTask();

        loading.IsVisible = true;

        if (result)
        {
            await OnDownloadData(); // Solo ejecutar si la operación fue completada (por ejemplo, después de guardar)
        }

        loading.IsVisible = false;
    }

    private async void OnDelete(CoreDTO secret)
    {
        loading.IsVisible = true;

        var result = await _passwordManagerService.Delete(secret.Id);
        CoreData.Remove(secret);

        loading.IsVisible = false;
    }

    private async Task OnDownloadData()
    {
        loading.IsVisible = true;
        SecretsCollectionView.ItemsSource = null;
        CoreData.Clear();

        var result = await _passwordManagerService.GetAll();
        var sortData = result.Data.OrderBy(dt => dt.Data01).ToList();

        foreach (var data in sortData)
        {
            CoreData.Add(data);
        }

        SecretsCollectionView.ItemsSource = CoreData;
        loading.IsVisible = false;
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
}
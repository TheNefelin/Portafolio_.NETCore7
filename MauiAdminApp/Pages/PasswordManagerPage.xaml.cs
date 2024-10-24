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
        var frame = (Frame)sender;
        // Animación para hacer "clic"
        await frame.ScaleTo(0.90, 100); // Reducir tamaño
        await frame.ScaleTo(1, 100); // Volver a tamaño original

        await OnDownloadData();
    }

    private async void OnCreate(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        // Animación para hacer "clic"
        await frame.ScaleTo(0.90, 100); // Reducir tamaño
        await frame.ScaleTo(1, 100); // Volver a tamaño original

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

    private async void OnDecryptData(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        // Animación para hacer "clic"
        await frame.ScaleTo(0.90, 100); // Reducir tamaño
        await frame.ScaleTo(1, 100); // Volver a tamaño original

        if (CoreData.Count == 0)
        {
            await DisplayAlert("Error", "La Lista esta Vacía.", "Ok");
            return;
        }

        for (int i = 0; i < CoreData.Count; i++)
            if (!IsBase64String(CoreData[i].Data01))
            {
                await DisplayAlert("Error", "La Lista ya está Descifrado.", "Ok");
                return;
            }

        // Mostrar la página modal y esperar el resultado
        var passwordPrompt = new PasswordPromptPage(_passwordManagerService);
        await Navigation.PushModalAsync(passwordPrompt);
        var (password, coreIV) = await passwordPrompt.GetPasswordAsync();

        loading.IsVisible = true;

        if (!string.IsNullOrEmpty(coreIV.IV))
        {
            EncryptionService encryptionService = new EncryptionService();

            for (int i = 0; i < CoreData.Count; i++)
                CoreData[i] = encryptionService.DecryptData(CoreData[i], password, coreIV.IV);
        }

        loading.IsVisible = false;
    }

    private async void OnEdit(CoreDTO coreDTO)
    {
        var page = new PasswordManagerFormPage(_passwordManagerService, coreDTO);
        await Navigation.PushAsync(page);
        var result = await page.GetCompletionTask();

        loading.IsVisible = true;

        if (result)
        {
            await OnDownloadData(); // Solo ejecutar si la operación fue completada (por ejemplo, después de guardar)
        }

        loading.IsVisible = false;
    }

    private async void OnDelete(CoreDTO coreDTO)
    {
        loading.IsVisible = true;

        var result = await _passwordManagerService.Delete(coreDTO.Id);
        CoreData.Remove(coreDTO);

        loading.IsVisible = false;
    }

    private async Task OnDownloadData()
    {
        loading.IsVisible = true;
        SecretsCollectionView.ItemsSource = null;
        CoreData.Clear();

        var (result, success) = await _passwordManagerService.GetAll();
        var sortData = result.Data.OrderBy(dt => dt.Data01).ToList();

        foreach (var data in sortData)
            CoreData.Add(data);

        SecretsCollectionView.ItemsSource = CoreData;
        loading.IsVisible = false;
    }

    public bool IsBase64String(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return false;

        Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
        return Convert.TryFromBase64String(base64String, buffer, out _);
    }
}
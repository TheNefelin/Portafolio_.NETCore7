using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class PasswordManagerFormPage : ContentPage
{
    private readonly PasswordManagerService _passwordManagerService;
	private readonly CoreDTO _coreDTO;
    private TaskCompletionSource<bool> _tcs; // Para manejar la tarea as�ncrona

    public PasswordManagerFormPage(PasswordManagerService passwordManagerService, CoreDTO coreDTO)
	{
		InitializeComponent();

		_passwordManagerService = passwordManagerService;
		_coreDTO = coreDTO;
        _tcs?.SetResult(false);

        LoadPage();
	}

	private async void LoadPage()
	{
		LoggedinDTO loggedinDTO = await AuthService.GetUser();

		if (_coreDTO != null)
		{
            IdEntry.Text = _coreDTO.Id.ToString();
			Data01Entry.Text = _coreDTO.Data01;
			Data02Entry.Text = _coreDTO.Data02;
			Data03Entry.Text = _coreDTO.Data03;
            BtnClick.Text = "Modificar";
        }
		else {
            IdEntry.Text = "0";
			BtnClick.Text = "Guardar";
        }
    }

	private async void OnSave(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(IdEntry.Text) || string.IsNullOrEmpty(Data01Entry.Text) || string.IsNullOrEmpty(Data02Entry.Text) || string.IsNullOrEmpty(Data03Entry.Text)) {
            ErrorLabel.Text = "Debes Rellenar Todos los Campos.";
            ErrorFrame.IsVisible = true;
            return;
        }

        CoreDTO coreDTO = new()
        {
            Id = Int32.Parse(IdEntry.Text),
            Data01 = Data01Entry.Text,
            Data02 = Data02Entry.Text,
            Data03 = Data03Entry.Text,
        };

        if (coreDTO.Id == 0)
        {
            var result = await _passwordManagerService.Create(coreDTO);
        }
        else
        {
            var result = await _passwordManagerService.Edit(coreDTO);
        }
         
        _tcs?.SetResult(true);
        await Navigation.PopAsync();
    }

    public Task<bool> GetCompletionTask()
    {
        _tcs = new TaskCompletionSource<bool>();
        return _tcs.Task;
    }
}
using ClassLibraryDTOs;
using MauiAdminApp.Services;

namespace MauiAdminApp.Pages;

public partial class ChatPage : ContentPage
{
    private readonly ApiUrlGrpService _apiUrlGrpService;

    public ChatPage()
	{
		InitializeComponent();
    }
}
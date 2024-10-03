using MauiAdmin.Pages;

namespace MauiAdmin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }
    }
}

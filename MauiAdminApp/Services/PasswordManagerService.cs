namespace MauiAdminApp.Services
{
    public class PasswordManagerService
    {
        private readonly HttpClient _httpClient;

        public PasswordManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}

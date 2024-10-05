using ClassLibraryDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MauiAdminApp.Services
{
    public class ApiUrlGrpService
    {
        private readonly HttpClient _httpClient;

        public ApiUrlGrpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UrlGrpDTO>> GetAll()
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.GetAsync("/api/url-grp");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<List<UrlGrpDTO>>>(jsonResponse);
                return objResponse.Data;
            }

            return null;
        }

        // Método para obtener el token almacenado y añadirlo en los headers
        private async Task AddAuthorizationHeader()
        {
            // Recupera el token almacenado de SecureStorage
            var apiToken = await SecureStorage.GetAsync("jwt_token");

            if (!string.IsNullOrEmpty(apiToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            }
        }
    }
}

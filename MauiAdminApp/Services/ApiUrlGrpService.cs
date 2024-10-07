using ClassLibraryDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        public async Task<UrlGrpDTO> Create(UrlGrpDTO urlGrp)
        {
            urlGrp.Id = 0;

            await AddAuthorizationHeader();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(urlGrp), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/url-grp", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<UrlGrpDTO>>(jsonResponse);
                return objResponse.Data;
            }

            return null;
        }

        public async Task<UrlGrpDTO> Update(UrlGrpDTO urlGrp)
        {
            await AddAuthorizationHeader();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(urlGrp), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/url-grp", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<UrlGrpDTO>>(jsonResponse);
                return objResponse.Data;
            }

            return null;
        }

        public async Task<UrlGrpDTO> Update(int id)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"/api/url-grp/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<UrlGrpDTO>>(jsonResponse);
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

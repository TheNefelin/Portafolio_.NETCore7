using ClassLibraryDTOs;
using Newtonsoft.Json;

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
            var response = await _httpClient.GetAsync("/api/url-grp");


            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<List<UrlGrpDTO>>>(jsonResponse);
                var urlGrps = objResponse.Data;

                return urlGrps;
            }

            return null;
        }
    }
}

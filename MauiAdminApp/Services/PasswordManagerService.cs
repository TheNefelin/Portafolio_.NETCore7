using ClassLibraryDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MauiAdminApp.Services
{
    public class PasswordManagerService
    {
        private readonly HttpClient _httpClient;

        public PasswordManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseApiDTO<List<CoreDTO>>> GetAll()
        {
            try
            {
                LoggedinDTO loggedinDTO = await AuthService.GetUser();

                AddAuthorizationHeader(loggedinDTO.ApiToken);

                var response = await _httpClient.GetAsync($"/api/core/get-all/{loggedinDTO.SqlToken},{loggedinDTO.Id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<List<CoreDTO>>>(jsonResponse);
                    return objResponse;
                }
                else
                {
                    // Manejo de respuesta cuando no es éxito
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorResponse}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en la obtencion de datos: {ex.Message}");
            }

            return null;
        }

        public async Task<ResponseApiDTO<CoreDTO>> Create(CoreDTO coreDTO)
        {
            try
            {
                LoggedinDTO loggedinDTO = await AuthService.GetUser();
                AddAuthorizationHeader(loggedinDTO.ApiToken);

                coreDTO.Id = 0;
                coreDTO.Id_User = loggedinDTO.Id;

                var jsonContent = new StringContent(JsonConvert.SerializeObject(coreDTO), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/api/core/{loggedinDTO.SqlToken}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<CoreDTO>>(jsonResponse);
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la creacion de datos: {ex.Message}");
            }

            return null;
        }

        public async Task<ResponseApiDTO<CoreDTO>> Edit(CoreDTO coreDTO)
        {
            try
            {
                LoggedinDTO loggedinDTO = await AuthService.GetUser();
                AddAuthorizationHeader(loggedinDTO.ApiToken);

                coreDTO.Id_User = loggedinDTO.Id;

                var jsonContent = new StringContent(JsonConvert.SerializeObject(coreDTO), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/core/{loggedinDTO.SqlToken}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<CoreDTO>>(jsonResponse);
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la creacion de datos: {ex.Message}");
            }

            return null;
        }

        public async Task<ResponseApiDTO<CoreDTO>> Delete(int id)
        {
            try {
                LoggedinDTO loggedinDTO = await AuthService.GetUser();
                AddAuthorizationHeader(loggedinDTO.ApiToken);

                var response = await _httpClient.DeleteAsync($"/api/core/{loggedinDTO.SqlToken},{loggedinDTO.Id},{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<CoreDTO>>(jsonResponse);
                    return objResponse;
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error en la eliminacion de datos: {ex.Message}");
            }

            return null;
        }

        // Método para obtener el token almacenado y añadirlo en los headers
        private void AddAuthorizationHeader(string apiToken)
        {
            if (!string.IsNullOrEmpty(apiToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            }
        }
    }
}

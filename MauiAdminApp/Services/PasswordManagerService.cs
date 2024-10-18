using Android.Media.TV;
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

        private async Task<ResponseApiDTO<T>> RequestApiQuery<T>(string uri, CoreDTO coreDTO)
        {
            try 
            {
                // se obtiene los datos de sesion del usuario
                LoggedinDTO loggedinDTO = await AuthService.GetUser();

                if (coreDTO != null)
                    coreDTO.Id_User = loggedinDTO.Id;

                // se prepara el body de la request
                CoreRequestDTO<dynamic> coreRequestDTO = new()
                {
                    Sql_Token = loggedinDTO.SqlToken,
                    Id_Usuario = loggedinDTO.Id,
                    Id = coreDTO != null ? coreDTO.Id : null,
                    CoreData = coreDTO,
                };

                // se prepara la cabecera de la request con el token de Bearer
                if (!string.IsNullOrEmpty(loggedinDTO.ApiToken))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedinDTO.ApiToken);

                var jsonContent = new StringContent(JsonConvert.SerializeObject(coreRequestDTO), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync(uri, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<T>>(jsonResponse);
                    return objResponse;
                }
                else
                {
                    // Manejo de respuesta cuando no es exitosa
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

        public async Task<ResponseApiDTO<List<CoreDTO>>> GetAll()
        {
            return await RequestApiQuery<List<CoreDTO>>($"/api/core/get-all", null);
        }

        public async Task<ResponseApiDTO<CoreDTO>> Create(CoreDTO coreDTO)
        {
            coreDTO.Id = 0;
            return await RequestApiQuery<CoreDTO>($"/api/core/insert", coreDTO);
        }

        public async Task<ResponseApiDTO<CoreDTO>> Edit(CoreDTO coreDTO)
        {
            return await RequestApiQuery<CoreDTO>($"/api/core/update", coreDTO);
        }

        public async Task<ResponseApiDTO<CoreDTO>> Delete(int id)
        {
            CoreDTO coreDTO = new() { Id = id };
            return await RequestApiQuery<CoreDTO>($"/api/core/delete", coreDTO);
        }
    }
}

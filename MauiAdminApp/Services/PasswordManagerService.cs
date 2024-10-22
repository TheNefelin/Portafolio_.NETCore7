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

        public async Task<(ResponseApiDTO<CoreIVDTO>, bool success)> Login(string password)
        {
            return await RequestApiQuery<CoreIVDTO>($"/api/core/login", null, password);
        }

        public async Task<(ResponseApiDTO<List<CoreDTO>>, bool success)> GetAll()
        {
            return await RequestApiQuery<List<CoreDTO>>($"/api/core/get-all", null, "");
        }

        public async Task<(ResponseApiDTO<CoreDTO>, bool success)> Create(CoreDTO coreDTO)
        {
            coreDTO.Id = 0;
            return await RequestApiQuery<CoreDTO>($"/api/core/insert", coreDTO, "");
        }

        public async Task<(ResponseApiDTO<CoreDTO>, bool success)> Edit(CoreDTO coreDTO)
        {
            return await RequestApiQuery<CoreDTO>($"/api/core/update", coreDTO, "");
        }

        public async Task<(ResponseApiDTO<CoreDTO>, bool success)> Delete(int id)
        {
            CoreDTO coreDTO = new() { 
                Id = id,
                Data01 = "na",
                Data02 = "na",
                Data03 = "na",
            };
            
            return await RequestApiQuery<CoreDTO>($"/api/core/delete", coreDTO, "");
        }

        private async Task<(ResponseApiDTO<T>, bool success)> RequestApiQuery<T>(string uri, CoreDTO coreDTO, string password)
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
                    Password = password,
                    CoreData = coreDTO,
                };

                // se prepara la cabecera de la request con el token de Bearer
                if (!string.IsNullOrEmpty(loggedinDTO.ApiToken))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedinDTO.ApiToken);

                var jsonContent = new StringContent(JsonConvert.SerializeObject(coreRequestDTO), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync(uri, jsonContent);

                bool success = response.IsSuccessStatusCode;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApiDTO<T>>(jsonResponse);

                return (objResponse, success);

                //if (response.IsSuccessStatusCode)
                //{
                //}
            }
            catch (Exception ex)
            {
                bool success = false;
                ResponseApiDTO<T> responseApiDTO = new()
                {
                    StatusCode = 500,
                    Message = $"Error en la obtencion de datos: {ex.Message}",
                };

                return (responseApiDTO, success);
            }
        }
    }
}

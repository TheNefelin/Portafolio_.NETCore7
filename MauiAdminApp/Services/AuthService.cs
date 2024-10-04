using ClassLibraryDTOs;
using Newtonsoft.Json;
using System.Text;

namespace MauiAdminApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseApiDTO<LoggedinDTO>> Login(string email, string password)
        {
            string endpoint = "/api/auth/login";  // Ruta relativa del endpoint

            LoginDTO loginDTO = new()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseApiDTO<LoggedinDTO>>(jsonResponse);

                    // Almacenar los tokens de manera segura
                    await SecureStorage.SetAsync("jwt_token", result.Data.ApiToken);
                    await SecureStorage.SetAsync("sql_token", result.Data.SqlToken);

                    return result;
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
                Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
            }

            return null;
        }
    }
}

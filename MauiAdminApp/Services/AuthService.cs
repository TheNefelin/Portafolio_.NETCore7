using ClassLibraryApplication.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace MauiAdminApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            //_httpClient = httpClient;
            _httpClient = new HttpClient();
        }

        public async Task<ResponseApiDTO<LoggedinDTO>> Login(string email, string password)
        {
            string endpoint = "api/auth/login";  // URL de la API

            LoginDTO loginDTO = new()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Hacer la solicitud POST
                var response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseApiDTO<LoggedinDTO>>(jsonResponse);

                    // Almacenar el JWT y SessionCode de manera segura
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
                // Manejo de errores
                Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
            }

            return null;
        }
    }
}


using ClassLibraryDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
                    await SecureStorage.SetAsync("id", result.Data.Id);
                    await SecureStorage.SetAsync("role", result.Data.Role);
                    await SecureStorage.SetAsync("expire_min", result.Data.ExpireMin);
                    await SecureStorage.SetAsync("jwt_token", result.Data.ApiToken);
                    await SecureStorage.SetAsync("sql_token", result.Data.SqlToken);
                    await SecureStorage.SetAsync("date_login", DateTime.Now.ToString("O"));

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

        public async Task<bool> ValidateToken(string jwtToken)
        {
            //string endpoint = "/api/auth/validate";  // Ruta relativa del endpoint de validación de token
            string endpoint = "/api/youtube";

            // Agregar el token en el encabezado de la petición
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    // Si el token es válido, la API responderá con éxito
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Token inválido o expirado. {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validando el token: {ex.Message}");
            }

            return false;
        }

        public static async Task RemoveToken()
        {
            await SecureStorage.SetAsync("jwt_token", string.Empty); // O puedes usar SecureStorage.RemoveAsync("jwt_token");
            await SecureStorage.SetAsync("sql_token", string.Empty); // Si deseas limpiar también este token
        }
    }
}

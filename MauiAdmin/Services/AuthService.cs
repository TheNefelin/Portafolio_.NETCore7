using Newtonsoft.Json;
using System.Text;

namespace MauiAdmin.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            string apiUrl = "https://artema.bsite.net/api/auth/login";  // URL de la API

            var loginData = new
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Hacer la solicitud POST
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                    // Almacenar el JWT y SessionCode de manera segura
                    await SecureStorage.SetAsync("jwt_token", result.JwtToken);
                    await SecureStorage.SetAsync("session_code", result.SessionCode);

                    return result;
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

    public class LoginResponse
    {
        [JsonProperty("jwt_token")]
        public string JwtToken { get; set; }

        [JsonProperty("session_code")]
        public string SessionCode { get; set; }
    }
}

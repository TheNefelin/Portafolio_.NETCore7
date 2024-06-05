namespace BibliotecaAuth.Classes
{
    public class LoginCredential
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AuthHash { get; set; } = string.Empty;
        public string AuthSalt { get; set; } = string.Empty;
        public string SessionCode { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}

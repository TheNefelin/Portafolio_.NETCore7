namespace BibliotecaAuth.Classes
{
    public class NewRegister
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string AuthHash { get; set; } = string.Empty;
        public string AuthSalt { get; set; } = string.Empty;
    }
}

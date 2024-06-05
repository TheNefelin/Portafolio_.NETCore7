namespace BibliotecaAuth.Classes
{
    public class AuthToken
    {
        public int ExpireMin { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

namespace ApplicationClassLibrary.Interfaces
{
    public interface IAuthPassword
    {
        (string Hash, string Salt) HashPassword(string password);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}

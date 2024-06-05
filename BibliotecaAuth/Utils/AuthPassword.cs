using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BibliotecaAuth.Utils
{
    public class AuthPassword
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
            string salted = Convert.ToBase64String(saltBytes);
            string hashed = NewHash(password, saltBytes);

            return (hashed, salted);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            string hashed = NewHash(password, saltBytes);

            return hashed == hashedPassword;
        }

        private string NewHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}

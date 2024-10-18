using ClassLibraryDTOs;
using System.Security.Cryptography;
using System.Text;

namespace MauiAdminApp.Services
{
    public class EncryptionService
    {
        public static CoreDTO EncryptData(CoreDTO coreDTO)
        {
            return new CoreDTO
            {
                Data01 = coreDTO.Data01,
                Data02 = coreDTO.Data02,
                Data03 = coreDTO.Data03,
            };
        }

        public static CoreDTO DecryptData(CoreDTO coreDTO)
        {
            return new CoreDTO
            {
                Data01 = coreDTO.Data01,
                Data02 = coreDTO.Data02,
                Data03 = coreDTO.Data03,
            };
        }

        private string Encrypt(string plainText, string pass, string iv)
        {
            using var aes = Aes.Create();
            aes.Key = GetAesKey(pass);
            aes.IV = Convert.FromBase64String(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            byte[] encrypted = ms.ToArray();

            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string encryptedText, string pass, string iv)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            aes.Key = GetAesKey(pass);
            aes.IV = Convert.FromBase64String(iv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        private byte[] GetAesKey(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            while (keyBytes.Length < 32)
                keyBytes = keyBytes.Concat(keyBytes).ToArray(); // Concatenamos la clave

            return keyBytes.Take(32).ToArray();
        }
    }
}

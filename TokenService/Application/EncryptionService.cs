using Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Application
{

    /// <summary>
    /// Does all of the encryptions. Would break thsi further down to single responsinility classes and rename to CustomEncryptionService.
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        public EncryptionService()
        {
        }

        public string Encrypt(string data, string secretKey)
        {
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data));
            var encodedSignature = Convert.ToBase64String(signature);
            return encodedSignature;
        }

        public HashSalt GenerateSaltedHash(string salt, string password)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var provider = new RNGCryptoServiceProvider();
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }

        public string GetSalt()
        {
            var random = new RNGCryptoServiceProvider();
            int max_length = 32;
            byte[] salt = new byte[max_length];
            random.GetNonZeroBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}

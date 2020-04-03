using Domain;

namespace Application
{
    public interface IEncryptionService
    {
        public string Encrypt(string data, string secretKey);
        public HashSalt GenerateSaltedHash(string salt, string password);
        public string GetSalt();
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
    }
}
using Domain.ValueObjects;

namespace Application.Common.Interfaces
{
    public interface IEncryptionService
    {
        public string Encrypt(string data, string secretKey);
        public HashSalt GenerateSaltedHashPassword(string salt, string password);
        public string GetSalt();
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
    }
}
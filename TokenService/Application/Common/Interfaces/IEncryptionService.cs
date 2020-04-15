using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Interfaces
{
    public interface IEncryptionService
    {
        public string ComputeHmac(string data, string secretKey,ALG alg );
        public HashSalt GenerateSaltedHashPassword(string salt, string password);
        public string GetSalt();
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
    }
}
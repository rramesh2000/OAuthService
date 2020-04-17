using Application.Common.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Behaviours
{

    /// <summary>
    /// TODO: Does all of the encryptions. Would break this further down to single responsibility classes and rename to CustomEncryptionService.
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        public EncryptionService()
        {
        }

        public string ComputeHmac(string data, string secretKey, ALG alg)
        {
            byte[] bdata = Encoding.UTF8.GetBytes(data);
            byte[] bkey = Encoding.UTF8.GetBytes(secretKey);

            switch (alg)
            {
                case ALG.HS256:
                    return Convert.ToBase64String(ComputeHmacsha256(bdata, bkey));
                case ALG.HS512:
                    return Convert.ToBase64String(ComputeHmacsha512(bdata, bkey));
                case ALG.HS384:
                    return Convert.ToBase64String(ComputeHmacsha384(bdata, bkey));
                case ALG.RS256:
                    return Convert.ToBase64String(ComputeRS256(bdata, bkey));
                default:
                    return Convert.ToBase64String(ComputeHmacsha256(bdata, bkey));
            }
        }

        public byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

        public byte[] ComputeHmacsha384(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA384(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

        public byte[] ComputeHmacsha512(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA512(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        public byte[] ComputeRS256(byte[] toBeHashed, byte[] key)
        {
            var privKeyObj = Asn1Object.FromByteArray(key);
            var privStruct = RsaPrivateKeyStructure.GetInstance((Asn1Sequence)privKeyObj);
            ISigner sig = SignerUtilities.GetSigner("SHA256withRSA");
            sig.Init(true, new RsaKeyParameters(true, privStruct.Modulus, privStruct.PrivateExponent));
            sig.BlockUpdate(toBeHashed, 0, toBeHashed.Length);
            return sig.GenerateSignature();
        }
        public HashSalt GenerateSaltedHashPassword(string salt, string password)
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

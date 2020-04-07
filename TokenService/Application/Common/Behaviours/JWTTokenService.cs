using Application.Common.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Application.Common.Behaviours
{
    //TODO: Refactor this using Facade pattern 
    public class JWTTokenService : ITokenService
    {
        public JWTTokenService(IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }

        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public string SecretKey { get; set; }

        public string GetToken(Users users)
        {
            int tokenExpiery = 5;
            Header header = new Header { alg = "HS256", typ = "JWT" }; //TODO: This needs to be moved to the configuration             
            Payload payload = new Payload
            {
                iss = "OAuthService",
                sub = users.UserName,
                exp = new TimeSpan(0, 0, tokenExpiery, 0, 0).TotalSeconds.ToString(),
                iat = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                nbf = DateTime.Now.AddMinutes(tokenExpiery).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                jti = Guid.NewGuid().ToString(),
                username = users.UserName,
                admin = true
            };


            JWTToken token = new JWTToken();
            token._header = header;
            token._payload = payload;

            string headerStr = JsonSerializer.Serialize(header);
            string payloadStr = JsonSerializer.Serialize(payload);
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey);
            string tokenStr = Base64Encode(headerStr) + "." + Base64Encode(payloadStr) + "." + signatureStr;
            return tokenStr;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public bool VerifyToken(string token)
        {
            bool tmpBool = false;
            string[] arr = token.Split('.');
            string headerStr = Base64Decode(arr[0]);
            string payloadStr = Base64Decode(arr[1]);
            string passedSignatureStr = arr[2];
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey);
            if (passedSignatureStr == signatureStr)
            {
                tmpBool = true;
            }
            return tmpBool;
        }


        public bool VerifyTokenTime(string token)
        {

            bool tmpBool = false;
            string[] arr = token.Split('.');
            string headerStr = Base64Decode(arr[0]);
            Payload payloadStr = JsonSerializer.Deserialize<Payload>(Base64Decode(arr[1]));
            if (DateTimeOffset.Parse(payloadStr.nbf).UtcDateTime >= DateTime.Now.ToUniversalTime())
            {
                tmpBool = true;
            }
            return tmpBool;
        }

        public string GetSignature(string headerStr, string payloadStr, string SecretKey)
        {
            string value = EncryptSvc.Encrypt(Base64Encode(headerStr) + "." + Base64Encode(payloadStr), SecretKey);
            return value;
        }

        //TODO:  Need to move below to a helper class 

        public string UrlEncode(string tmpStr)
        {
            return System.Web.HttpUtility.UrlEncode(tmpStr);
        }

        public string Base64Encode(string tmpStr)
        {
            string base64Encoded;
            byte[] data = Encoding.UTF8.GetBytes(tmpStr);
            base64Encoded = System.Convert.ToBase64String(data);
            return base64Encoded;
        }

        public string Base64Decode(string tmpStr)
        {
            string base64Decoded;
            byte[] data = Convert.FromBase64String(tmpStr);
            base64Decoded = Encoding.UTF8.GetString(data);
            return base64Decoded;
        }
    }
}

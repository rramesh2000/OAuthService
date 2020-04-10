using Application.Common.Behaviours.JWT;
using Application.Common.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text.Json;

namespace Application.Common.Behaviours
{
    //TODO: Refactor this using Facade pattern 
    public class JWTTokenService : BaseService, ITokenService
    {
        public IEncryptionService EncryptSvc { get; set; }

        public string SecretKey { get; set; }

        public IConfiguration Configuration { get; set; }

        public JWTTokenService(IEncryptionService encryptSvc, IConfiguration configuration)
        {

            EncryptSvc = encryptSvc;
            config = configuration;
            SecretKey = config["Secretkey"];
        }

        public string GenerateAccessToken(Users users)
        {
            int tokenExpiery = Int32.Parse(config["AccessTokenLife"]);
            Header header = new Header
            {
                alg = config.GetValue<string>("jwt:header:alg"),
                typ = config.GetValue<string>("jwt:header:typ")
            };
            Payload payload = new Payload
            {
                iss = config.GetValue<string>("jwt:payload:iss"),
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
            string tokenStr = headerStr.Base64Encode() + "." + payloadStr.Base64Encode() + "." + signatureStr;
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

        public bool VerifyAccessToken(string token)
        {
            bool tmpBool = false;
            string[] arr = token.Split('.');
            string headerStr = arr[0].Base64Decode();
            string payloadStr = arr[1].Base64Decode();
            string passedSignatureStr = arr[2];
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey);
            if (passedSignatureStr == signatureStr)
            {
                tmpBool = true;
            }
            return tmpBool;
        }

        public bool VerifyAccessTokenTime(string token)
        {
            bool tmpBool = false;
            string[] arr = token.Split('.');
            string headerStr = arr[0].Base64Decode();
            Payload payloadStr = JsonSerializer.Deserialize<Payload>(arr[1].Base64Decode());
            if (DateTimeOffset.Parse(payloadStr.nbf).UtcDateTime >= DateTime.Now.ToUniversalTime())
            {
                tmpBool = true;
            }
            return tmpBool;
        }

        private string GetSignature(string headerStr, string payloadStr, string SecretKey)
        {
            string value = EncryptSvc.Encrypt(headerStr.Base64Encode() + "." + payloadStr.Base64Encode(), SecretKey);
            return value;
        }
    }
}

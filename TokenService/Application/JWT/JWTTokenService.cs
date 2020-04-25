using Application.Common.Interfaces;
using Application.Common.Models;
using Application.JWT.Extensions;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text.Json;
using System.Web;
namespace Application.JWT
{

    public class JWTTokenService : ITokenService
    {
        public string SecretKey { get; set; }
        public string algorithm { get; set; }
        public IEncryptionService EncryptSvc { get; set; }
        public IConfiguration config { get; set; }

        public JWTTokenService(ITSLogger log, IEncryptionService encryptSvc, IConfiguration configuration)
        {
            EncryptSvc = encryptSvc;
            config = configuration;
            SecretKey = config["Secretkey"];
            algorithm = config.GetValue<string>("jwt:header:alg");
        }

        public string GenerateAccessToken(UserDTO user)
        {
            int tokenExpiery = int.Parse(config["AccessTokenLife"]);
            Header header = new Header
            {
                alg = algorithm,
                typ = config.GetValue<string>("jwt:header:typ")
            };

            Payload payload = new Payload
            {
                iss = config.GetValue<string>("jwt:payload:iss"),
                sub = user.UserName,
                exp = new TimeSpan(0, 0, tokenExpiery, 0, 0).TotalSeconds.ToString(),
                iat = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                nbf = DateTime.Now.AddMinutes(tokenExpiery).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                jti = Guid.NewGuid().ToString(),
                username = user.UserName,
                admin = true
            };

            JWTToken token = new JWTToken
            {
                _header = header,
                _payload = payload
            };

            string headerStr = JsonSerializer.Serialize(header);
            string payloadStr = JsonSerializer.Serialize(payload);
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey, (ALG)Enum.Parse(typeof(ALG), algorithm, true));
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
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey, (ALG)Enum.Parse(typeof(ALG), algorithm, true));
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

        private string GetSignature(string headerStr, string payloadStr, string SecretKey, ALG alg)
        {
            string value = EncryptSvc.ComputeHmac(headerStr.Base64Encode() + "." + payloadStr.Base64Encode(), SecretKey, alg);
            return value;
        }

    }
}

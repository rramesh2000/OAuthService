using Application.Common.Interfaces;
using Application.Common.Models;
using Application.JWT.Extensions;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
namespace Application.JWT
{

    public class JWTToken : ITokenService
    {
        public string SecretKey { get; set; }
        public string algorithm { get; set; }
        public IEncryptionService EncryptSvc { get; set; }
        public IConfiguration config { get; set; }

        public JWTToken(ITSLogger log, IEncryptionService encryptSvc, IConfiguration configuration)
        {
            EncryptSvc = encryptSvc;
            config = configuration;
            SecretKey = config["Secretkey"];
            algorithm = config.GetValue<string>("jwt:header:alg");
        }

        public string GenerateToken(TokenDTO tokenDTO)
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
                sub = tokenDTO.UserName,
                exp = new TimeSpan(0, 0, tokenExpiery, 0, 0).TotalSeconds.ToString(),
                iat = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                nbf = DateTime.Now.AddMinutes(tokenExpiery).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                jti = Guid.NewGuid().ToString(),
                username = tokenDTO.UserName,
                admin = true
            };

            string headerStr = JsonSerializer.Serialize(header);
            string payloadStr = JsonSerializer.Serialize(payload);
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey, (ALG)Enum.Parse(typeof(ALG), algorithm, true));
            string tokenStr = headerStr.Base64Encode() + "." + payloadStr.Base64Encode() + "." + signatureStr;
            return tokenStr;
        }

        public bool VerifyToken(string token)
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

        public bool VerifyTokenTime(string token)
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

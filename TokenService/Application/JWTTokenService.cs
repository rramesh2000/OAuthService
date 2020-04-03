using Domain;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Application
{
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
            Header header = new Header { alg = "HS256", typ = "JWT" }; // This needs to be moved to the configuration 
            Payload payload = new Payload { username = users.UserName, admin = true }; //This "admin" needs to be changed to a role claim.

            JWTToken token = new JWTToken();
            token._header = header;
            token._payload = payload;

            string headerStr = JsonSerializer.Serialize(header);
            string payloadStr = JsonSerializer.Serialize(payload);
            string signatureStr = GetSignature(headerStr, payloadStr, SecretKey);
            string tokenStr = Base64Encode(headerStr) + "." + Base64Encode(payloadStr) + "." + signatureStr;
            return tokenStr;
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
        public string GetSignature(string headerStr, string payloadStr, string SecretKey)
        {
            string value = EncryptSvc.Encrypt(Base64Encode(headerStr) + "." + Base64Encode(payloadStr), SecretKey);
            return value;
        }
        
        // Need to move below to a helper class 

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

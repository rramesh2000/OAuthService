using Domain;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Application
{
    public class TokenValidationService : BaseService
    {

        public TokenValidationService()
        {
        }

        public TokenValidationService( string secretKey)
        {
            EncryptSvc = new EncryptionService();
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }

        public TokenValidationService(IEncryptionService encryptSvc, string secretKey)
        {
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }

        public TokenValidationService(IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }

        public TokenValidationService(ITokenService jWTTokenService, IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            JWTTokenService = jWTTokenService;
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }

        public ITokenService JWTTokenService { get; set; }
        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public string SecretKey { get; set; }     

        public bool VerifyToken(string authorization)
        {
            return JWTTokenService.VerifyToken(authorization);
        }
             
    }
}

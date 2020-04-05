using Domain;
using Infrastructure.Models;
using System;
using FluentValidation.Results;

namespace Application
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {

        public AuthenticationService()
        {
        }

        public AuthenticationService(string secretKey)
        {
            EncryptSvc =  new EncryptionService();
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }


        public AuthenticationService(IEncryptionService encryptSvc, string secretKey)
        {
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }

        public AuthenticationService(IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }

        public AuthenticationService(ITokenService jWTTokenService, IDBService dBService, IEncryptionService encryptSvc, string secretKey)
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

        public string Authenticate(UserLogin userLogin)
        {
            string Authorization = String.Empty;
            ValidationResult results = userloginvalidation.Validate(userLogin);
            Users users = mapper.Map<Users>(DBService.GetUser(userLogin.username));
         
            if (users != null)
            {
                if (CheckIsUser(userLogin, users))
                {
                    Authorization = JWTTokenService.GetToken(users);
                }
            }
            else
            {
                Authorization = "Unauthorised";
            }
            return Authorization;
        }

        private bool CheckIsUser(UserLogin userLogin, Users users)
        {
            bool tmp = false;

            if (users != null)
            {
                var hash = EncryptSvc.GenerateSaltedHash(users.Salt, userLogin.password);
                Log.Log.Information(hash.ToString());  //TODO: remove after test case works 
                if (EncryptSvc.VerifyPassword(userLogin.password, hash.Hash, hash.Salt))
                {
                    tmp = true;
                }
            }
            return tmp;
        }

    }
}

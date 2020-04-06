using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.TokenValidation.Handlers;

namespace Application.TokenValidation
{
    public class TokenValidationService : BaseService
    {
        public ITokenService JWTTokenService { get; set; }
        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public string SecretKey { get; set; }

        public TokenValidationService()
        {
        }

        public TokenValidationService(string secretKey)
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


        public bool VerifyToken(string authorization)
        {
            try
            {
                AuthorizationVm authorizationVm = new AuthorizationVm(authorization, false, SecretKey, JWTTokenService);
                TokenValidationHandler h1 = new TokenVerificationHandler();
                TokenValidationHandler h2 = new TokenRevocationHandler();
                h1.SetSuccessor(h2);
                h1.HandleRequest(authorizationVm);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

    }
}

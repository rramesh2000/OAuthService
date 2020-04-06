using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TokenValidation.Handlers;
using Application.TokenValidation.Models;

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


        public string VerifyToken(string authorization)
        {
            try
            {
                AuthorizationDTO authorizationVm = new AuthorizationDTO(authorization, false, SecretKey, JWTTokenService);
                var handler = new TokenVerificationHandler();
                handler.SetNext(new TokenRevocationHandler());
                handler.Handle(authorizationVm);
                return "Valid token"; 
            }
            catch
            {
                throw new InvalidTokenException("Invalid Token");
            }
            
        }

    }
}

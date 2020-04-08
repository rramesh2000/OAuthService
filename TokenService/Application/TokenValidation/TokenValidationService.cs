using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TokenValidation.Handlers;
using Application.TokenValidation.Models;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Application.TokenValidation
{
    public class TokenValidationService : BaseService
    {
        public ITokenService JWTTokenService { get; set; }
        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public string configuration { get; set; }

        public TokenValidationService()
        {
        }

        public TokenValidationService(IConfiguration configuration)
        {
            EncryptSvc = new EncryptionService();

            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, configuration);
        }

        public TokenValidationService(IEncryptionService encryptSvc, IConfiguration configuration)
        {
            EncryptSvc = encryptSvc;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, configuration);
        }

        public TokenValidationService(IDBService dBService, IEncryptionService encryptSvc, IConfiguration configuration)
        {
            DBService = dBService;
            EncryptSvc = encryptSvc;
        }

        public TokenValidationService(ITokenService jWTTokenService, IDBService dBService, IEncryptionService encryptSvc, IConfiguration configuration)
        {
            JWTTokenService = jWTTokenService;
            DBService = dBService;
            EncryptSvc = encryptSvc;

        }

        public string VerifyToken(AccessTokenDTO auth)
        {
            try
            {
                string SecretKey = config["Secretkey"];
                AuthorizationDTO authorizationVm = new AuthorizationDTO(auth.Authorization, false, SecretKey, JWTTokenService);
                var handler = new TokenVerificationHandler();
                handler.SetNext(new TokenTimeVerificationHandler()).SetNext(new TokenRevocationHandler());
                handler.Handle(authorizationVm);
                return TokenConstants.ValidToken;
            }
            catch
            {
                throw new InvalidTokenException(TokenConstants.InvalidToken);
            }

        }

    }
}

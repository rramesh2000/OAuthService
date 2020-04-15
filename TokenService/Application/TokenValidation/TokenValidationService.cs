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
        public TokenValidationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
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

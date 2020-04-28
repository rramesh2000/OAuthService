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
        public TokenValidationService(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }

        public string VerifyToken(AccessTokenDTO auth)
        {
            try
            {
                string SecretKey = config["Secretkey"];
                ResponseDTO authorizationVm = new ResponseDTO(auth.Authorization, false, SecretKey, JWTToken);
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

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
        public IEncryptionService EncryptSvc { get; set; }
        public IConfiguration configuration { get; set; }

        public TokenValidationService(ITSLogger log, IConfiguration configuration) : base(log)
        {
            EncryptSvc = new EncryptionService();
            this.configuration = configuration;
            JWTTokenService = new JWTTokenService(log, EncryptSvc, this.configuration);
        }

        public string VerifyToken(AccessTokenDTO auth)
        {
            try
            {
                string SecretKey = configuration["Secretkey"];
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

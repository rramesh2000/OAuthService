using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Application.Authentication
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticateUser authenticateUser { get; set; }

        public AuthenticateRefresh authenticateRefresh { get; set; }

        public string SecretKey { get; set; }
        public AuthenticationService(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }


        public AuthenticationDTO Authenticate(AuthorizationGrantRequestDTO authorizationGrantRequestDTO)
        {
            IAuthenticate authenticate;

            switch (authorizationGrantRequestDTO.Grant_Type)
            {
                case AuthorizationGrantType.authorization_code:
                    authenticate = new AuthenticateCode(refreshtoken, config, Log, JWTToken, oauth, EncryptSvc);
                    break;
                case AuthorizationGrantType.refresh_token:
                    authenticate = new AuthenticateRefresh(refreshtoken, config, Log, JWTToken, oauth, EncryptSvc);
                    break;
                case AuthorizationGrantType.password:
                    authenticate = new AuthenticateUser(refreshtoken, config, Log, JWTToken, oauth, EncryptSvc);
                    break;
                default:
                    authenticate = new AuthenticateCode(refreshtoken, config, Log, JWTToken, oauth, EncryptSvc);
                    break;
            }

            TokenDTO tokenDTO = mapper.Map<TokenDTO>(authorizationGrantRequestDTO);
            return authenticate.AuthenticateGetToken(tokenDTO);
        }
    }
}

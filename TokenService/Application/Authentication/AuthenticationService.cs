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
        public AuthenticationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
            
        }
 

        public AuthenticationDTO Authenticate(AuthorizationGrantRequestDTO token)
        {
            IAuthenticate authenticate;
            switch (token.Grant_Type)
            {
                case AuthorizationGrantType.authorization_code:
                    authenticate = new AuthenticateCode(config,Log, JWTTokenService, oauth,EncryptSvc);
                    break;
                case AuthorizationGrantType.refresh_token:
                    authenticate = new AuthenticateRefresh(config, Log, JWTTokenService, oauth, EncryptSvc);
                    break;
                case AuthorizationGrantType.password:
                    authenticate = new AuthenticateUser(config, Log, JWTTokenService, oauth, EncryptSvc);
                    break;
                default:
                    authenticate = new AuthenticateCode(config, Log, JWTTokenService, oauth, EncryptSvc);                    
                    break;
            }
            return authenticate.AuthenticateGetToken(token);           
        }
    }
}

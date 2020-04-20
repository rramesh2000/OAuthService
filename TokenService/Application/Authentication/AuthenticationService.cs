using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticateUser authenticateUser { get; set; }

        public AuthenticateRefresh authenticateRefresh { get; set; }

        public string SecretKey { get; set; }
        public AuthenticationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
            authenticateRefresh = new AuthenticateRefresh(configuration, log, jWTTokenService, oauth, encryptSvc);
            authenticateUser = new AuthenticateUser(configuration, log, jWTTokenService, oauth, encryptSvc);
        }

        public AuthenticationDTO AuthenticateUserLogin(UserDTO userLogin)
        {
            return authenticateUser.AuthenticateUserLogin(userLogin);
        }

        public AuthenticationDTO AuthenticateRefreshToken(RefreshTokenDTO refauth)
        {
            return authenticateRefresh.AuthenticateRefreshToken(refauth);
        }


    }
}

using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDTO AuthenticateUserLogin(UserLoginDTO userLogin);
        AuthenticationDTO AuthenticateRefreshToken(RefreshTokenDTO refauth);
    }
}
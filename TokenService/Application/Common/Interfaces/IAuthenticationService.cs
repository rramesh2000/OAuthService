using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDTO AuthenticateUserLogin(UserDTO userLogin);
        AuthenticationDTO AuthenticateRefreshToken(RefreshTokenDTO refauth);
    }
}
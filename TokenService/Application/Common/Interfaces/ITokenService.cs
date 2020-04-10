using Infrastructure.Models;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(Users users);
        string GenerateRefreshToken();
        bool VerifyAccessToken(string token);
        bool VerifyAccessTokenTime(string token);
    }
}
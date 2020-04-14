using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDTO user);
        string GenerateRefreshToken();
        bool VerifyAccessToken(string token);
        bool VerifyAccessTokenTime(string token);
    }
}
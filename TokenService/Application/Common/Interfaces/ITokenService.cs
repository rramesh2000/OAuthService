using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {

        string GenerateToken(TokenDTO tokenDTO);

        bool VerifyToken(string token);

        bool VerifyTokenTime(string token);
    }
}
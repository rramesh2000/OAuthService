using Infrastructure.Models;

namespace Application
{
    public interface ITokenService
    {
        string GetToken(Users users);
        bool VerifyToken(string token);
    }
}
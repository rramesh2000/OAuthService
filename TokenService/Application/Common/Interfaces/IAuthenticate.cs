using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthenticate
    {
        string SecretKey { get; set; }

        AuthenticationDTO AuthenticateGetToken(TokenDTO tokenDTO);
    }
}
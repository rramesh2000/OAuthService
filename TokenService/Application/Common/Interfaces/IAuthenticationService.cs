using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
          AuthenticationDTO Authenticate(AuthorizationGrantRequestDTO token);
    }
}
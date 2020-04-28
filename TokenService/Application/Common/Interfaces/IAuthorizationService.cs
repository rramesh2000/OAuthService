using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        AuthorizationResponseDTO GetAuthorization(AuthorizationRequestDTO authorizeDTO);
    }
}
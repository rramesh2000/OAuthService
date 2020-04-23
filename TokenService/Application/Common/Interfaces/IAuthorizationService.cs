using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        AuthorizationResponseDTO GetAuthorization(AuthorizationRequestDTO authorizeDTO);
    }
}
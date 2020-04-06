using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(UserLogin userLogin);
    }
}
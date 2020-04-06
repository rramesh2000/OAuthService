using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(UserLoginDTO userLogin);
    }
}
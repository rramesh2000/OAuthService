using Domain;
using Infrastructure.Models;

namespace Application
{
    public interface IAuthenticationService
    {
        string Authenticate(UserLogin userLogin);     
    }
}
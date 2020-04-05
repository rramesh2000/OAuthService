using Infrastructure.Models;

namespace Application
{
    public interface IDBService
    {
        OAuthContext oauth { get; set; }

        Users GetUser(string username);
        Users SaveUser(Users user);
    }
}
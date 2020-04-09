using Domain.Entities;
using Infrastructure.Models;

namespace Application.Common.Interfaces
{
    public interface IDBService
    {
        OAuthContext oauth { get; set; }
        Users GetUser(string username);
        Users SaveUser(User user);
        Users GetUserFromRefreshToken(string authorization);
        void UpdateUserRefreshToken(string username, string refresh_token);
    }
}
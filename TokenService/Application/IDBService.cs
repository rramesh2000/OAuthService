using Domain;
using Infrastructure.Models;

namespace Application
{
    public interface IDBService
    {
        Users GetUser(string username);
        bool SaveUser(Users user);
    }
}
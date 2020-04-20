using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface ITokenServiceDbContext
    {
        DbSet<User> User { get; set; }
        DbSet<Authorize> Authorize { get; set; }
        DbSet<Client> Client { get; set; }
        public int SaveChanges();
    }
}

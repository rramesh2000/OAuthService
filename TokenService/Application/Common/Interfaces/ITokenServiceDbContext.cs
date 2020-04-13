using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface ITokenServiceDbContext
    {
        DbSet<User> User { get; set; }
        public int SaveChanges();
    }
}

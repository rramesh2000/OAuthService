using Application.Common.Behaviours;
using Domain.Entities;
using System;

namespace Application.Common.Models
{
    public class UserLoginDTO
    {
        public UserLoginDTO()
        {
            this.UserName = UserName;
            this.password = password;
            this.users = new User();
            this.encryptionService = new EncryptionService();
        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Salt { get; set; }
        public User users { get; set; }

        public string HashPassword { get; set; }
        public string RefreshToken { get; set; }
        public EncryptionService encryptionService { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(Guid userId, string username, string password)
        {
            UserId = userId;
            Username = username;
            Password = password;
        }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public string RefreshToken { get; set; }
    }
}

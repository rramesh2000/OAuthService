using System;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(Guid userId, string username)
        {
            UserId = userId;
            UserName = username;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public string RefreshToken { get; set; }
    }
}

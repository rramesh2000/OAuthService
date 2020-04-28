using System;

namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public string RefreshToken { get; set; }
        public User()
        {
        }

        public User(Guid userId, string username)
        {
            UserId = userId;
            UserName = username;
        }



    }
}

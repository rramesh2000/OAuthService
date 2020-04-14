using System;

namespace Application.Common.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
            this.UserName = UserName;
            this.password = password;
        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public string RefreshToken { get; set; }
    }
}

using Domain.Enums;
using System;
using System.Runtime.Serialization;

namespace Application.Common.Models
{
    public class UserDTO
    {
        public AuthorizationGrantType Grant_Type { get; set; }

        [IgnoreDataMember]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        [IgnoreDataMember]
        public string Salt { get; set; }
        [IgnoreDataMember]
        public string HashPassword { get; set; }
        [IgnoreDataMember]
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }

        public UserDTO()
        {
            this.UserName = UserName;
            this.password = password;
            IsAuthenticated = false;
        }
    }
}

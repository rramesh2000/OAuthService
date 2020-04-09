using System;
using System.Collections.Generic;

namespace Infrastructure.Infrastructure.Models
{
    public partial class Users
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public string HashPassword { get; set; }
    }
}

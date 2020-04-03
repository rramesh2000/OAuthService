using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public partial class Users
    {
        public Guid UserId { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
    }
}

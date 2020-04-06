using Application.Common.Behaviours;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{ 
      public class UserLoginDTO
    {
        public string username { get; set; }
        public string password { get; set; }

        public Users users { get; set; }

        public EncryptionService encryptionService { get; set; }
    }
}

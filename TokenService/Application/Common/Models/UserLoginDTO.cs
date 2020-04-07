using Application.Common.Behaviours;
using Infrastructure.Models;

namespace Application.Common.Models
{
    public class UserLoginDTO
    {
        public UserLoginDTO()
        {
            this.username = username;
            this.password = password;
            this.users =  new Users();
            this.encryptionService = new EncryptionService();
        }

        public string username { get; set; }
        public string password { get; set; }

        public Users users { get; set; }

        public EncryptionService encryptionService { get; set; }
    }
}

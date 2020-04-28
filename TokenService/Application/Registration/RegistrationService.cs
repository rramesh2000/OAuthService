using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Configuration;

namespace Application.Registration
{
    public class RegistrationService : BaseService
    {
        public RegisterUser registerUser { get; set; }

        public RegisterClient registerClient { get; set; }

        public RegistrationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
            registerUser = new RegisterUser(configuration, log, jWTTokenService, oauth, encryptSvc);
            registerClient = new RegisterClient(configuration, log, jWTTokenService, oauth, encryptSvc);
        }

        public ClientDTO SaveClient(ClientDTO clientDTO)
        {
            return registerClient.SaveClient(clientDTO);
        }

        public UserDTO SaveUser(UserDTO userdto)
        {
            return registerUser.SaveUser(userdto);
        }
    }
}

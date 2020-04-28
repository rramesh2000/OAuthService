using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Application.Clients
{
    public class ClientService : BaseService, IClientService
    {
        public ClientService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }

        public ClientDTO GetClient(Guid Client_Id)
        {
            ClientDTO clientDTO = new ClientDTO();
            try
            {
                Client client = oauth.Client.SingleOrDefault(x => x.Client_Id == Client_Id);
                clientDTO = mapper.Map<ClientDTO>(client);
            }
            catch { }
            return clientDTO;
        }

    }
}

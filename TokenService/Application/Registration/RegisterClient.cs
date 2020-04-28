using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Application.Registration
{
    public class RegisterClient : BaseService
    {
        public RegisterClient(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }

        public ClientDTO SaveClient(ClientDTO clientdto)
        {
            try
            {
                ValidationResult results = clientvalidation.Validate(clientdto);
                if (!results.IsValid)
                {
                    string failures = string.Empty;
                    //TODO: Use projection and remove the for loop 
                    foreach (var failure in results.Errors)
                    {
                        failures += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                    }
                    throw new InvalidClientException(failures);
                }
                else
                {
                    Client client = mapper.Map<Client>(clientdto);
                    client.Client_Id = Guid.NewGuid();
                    client.Client_Secret = EncryptSvc.GetSalt();
                    if (oauth.Client.Where(x => x.Client_Id == client.Client_Id).Count() < 1)
                    {
                        oauth.Client.Add(client);
                        oauth.SaveChanges();
                    }
                    else
                    {
                        throw new DuplicateWaitObjectException();
                    }
                    clientdto = mapper.Map<ClientDTO>(client);
                }
            }
            catch (InvalidClientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.CannotCreateClient);
                throw new InvalidClientException(TokenConstants.CannotCreateClient);
            }
            return clientdto;
        }
    }
}

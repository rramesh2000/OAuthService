using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Exceptions;
using Domain.Common;
using Domain.Entities;
using FluentValidation.Results;
using Infrastructure.Models;
using System;
using System.Net;

namespace Application.Registration
{
    public class RegistrationService : BaseService
    {
        public RegistrationService() : base()
        {
            EncryptSvc = new EncryptionService();
            DBService = new DBMSSQLService();
        }

        public RegistrationService(IEncryptionService encryptSvc)
        {
            DBService = new DBMSSQLService();
            EncryptSvc = encryptSvc;
        }

        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public UserDTO SaveUser(UserDTO userdto)
        {

            try
            {
                User user = mapper.Map<User>(userdto);
                ValidationResult results = uservalidation.Validate(user);
                if (!results.IsValid)
                {
                    string failures = String.Empty;
                    foreach (var failure in results.Errors)
                    {
                        //TODO: Use projection and remove the for loop 
                        failures += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                    }
                    throw new InvalidUserException(failures);
                }
                else
                {
                    Users _user = mapper.Map<Users>(user);
                    _user.UserId = Guid.NewGuid();
                    _user.Salt = EncryptSvc.GetSalt();
                    _user.HashPassword = EncryptSvc.GenerateSaltedHash(_user.Salt, user.Password).Hash;

                    if (DBService.SaveUser(_user) != null)
                    {
                        response.Body += "Success";
                        response.httpstatus = HttpStatusCode.Created;
                    }
                }
            }
            catch (InvalidUserException exUser)
            {
                throw new InvalidUserException(exUser.Message);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, "Cannot create User"); 
                throw new InvalidUserException("Cannot create User");
            }
            return userdto;
        }

    }
}

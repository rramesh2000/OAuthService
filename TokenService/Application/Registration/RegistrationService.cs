using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation.Results;
using System;

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
                    //TODO: Use projection and remove the for loop 
                    foreach (var failure in results.Errors)
                    {
                        failures += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                    }
                    throw new InvalidUserException(failures);
                }
                else
                {
                    user.UserId = Guid.NewGuid();
                    user.Salt = EncryptSvc.GetSalt();
                    user.HashPassword = EncryptSvc.GenerateSaltedHash(user.Salt, user.Password).Hash;
                    user = mapper.Map<User>(DBService.SaveUser(user));
                    userdto = mapper.Map<UserDTO>(user);
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

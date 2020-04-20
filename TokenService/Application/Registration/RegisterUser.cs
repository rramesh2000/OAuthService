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
    public class RegisterUser : BaseService
    {
        public RegisterUser(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }

        public UserDTO SaveUser(UserDTO userdto)
        {
            try
            {
                ValidationResult results = uservalidation.Validate(userdto);
                if (!results.IsValid)
                {
                    string failures = string.Empty;
                    //TODO: Use projection and remove the for loop 
                    foreach (var failure in results.Errors)
                    {
                        failures += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                    }
                    throw new InvalidUserException(failures);
                }
                else
                {
                    User user = mapper.Map<User>(userdto);
                    user.UserId = Guid.NewGuid();
                    user.Salt = EncryptSvc.GetSalt();
                    user.HashPassword = EncryptSvc.GenerateSaltedHashPassword(user.Salt, userdto.password).Hash;
                    if (oauth.User.Where(x => x.UserName == user.UserName).Count() < 1)
                    {
                        oauth.User.Add(user);
                        oauth.SaveChanges();
                    }
                    else
                    {
                        throw new DuplicateWaitObjectException();
                    }
                    userdto = mapper.Map<UserDTO>(user);
                }
            }
            catch (InvalidUserException exUser)
            {
                throw new InvalidUserException(exUser.Message);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.CannotCreateUser);
                throw new InvalidUserException(TokenConstants.CannotCreateUser);
            }
            return userdto;
        }
    }
}

﻿using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using System;

namespace Application.Registration
{
    public class RegistrationService : BaseService
    {
        public RegistrationService(ITSLogger log) : base(log)
        {
        }

        public DBMSSQLService DBService { get; set; }

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
                    user.HashPassword = EncryptSvc.GenerateSaltedHashPassword(user.Salt, userdto.Password).Hash;
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
                Log.Log.Error(ex, TokenConstants.CannotCreateUser);
                throw new InvalidUserException(TokenConstants.CannotCreateUser);
            }
            return userdto;
        }

    }
}

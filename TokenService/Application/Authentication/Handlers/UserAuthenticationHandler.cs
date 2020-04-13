using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.TokenValidation.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Handlers
{
    public class UserAuthenticationHandler : Handler<UserLoginDTO>
    {
        public object EncryptSvc { get; private set; }

        public override void Handle(UserLoginDTO userLogin)
        {
            var hash = userLogin.encryptionService.GenerateSaltedHashPassword(userLogin.Salt, userLogin.password);
            //TODO: Need to impliment this 
            if (!userLogin.encryptionService.VerifyPassword(userLogin.password, hash.Hash, hash.Salt))
            {
                throw new InvalidUserException("Invalid User");
            }
            base.Handle(userLogin);
        }
 
    }
}

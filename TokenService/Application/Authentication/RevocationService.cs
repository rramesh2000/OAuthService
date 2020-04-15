using Application.Authentication.Handlers;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Application.Authentication
{
    public class RevocationService : BaseService, IRevocationService
    {
        public RevocationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }
        public string TokenRevocation(RevocationDTO revocationDTO)
        {
            try
            {
                ValidationResult results1 = userloginvalidation.Validate(revocationDTO.user);
                ValidationResult results2 = refreshvalidation.Validate(revocationDTO.refresh);

                User user = oauth.User.Where(x => x.UserName == revocationDTO.user.UserName).FirstOrDefault();
                UserDTO userLoginDTO = mapper.Map<UserDTO>(user);
                userLoginDTO.password = revocationDTO.user.password;

                //Check user is authenticated
                var handler = new UserAuthenticationHandler();
                handler.Handle(userLoginDTO);

                revocationDTO.user = userLoginDTO;

                //Check refresh token provided is real
                var refreshhandler = new RefreshTokenAuthenticationHandler();
                refreshhandler.Handle(revocationDTO);

                //Set the refresh token to null
                user.RefreshToken = null;
                oauth.SaveChanges();
                return TokenConstants.RevokedToken;
            }
            catch (InvalidTokenException) { throw; }
            catch (InvalidUserException) { throw; }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
        }
    }
}

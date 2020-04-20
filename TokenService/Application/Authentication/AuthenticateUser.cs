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

namespace Application
{
    public class AuthenticateUser : BaseService 
    {


        public string SecretKey { get; set; }
        public AuthenticateUser(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
         
        }

        public AuthenticationDTO AuthenticateUserLogin(UserDTO userLogin)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                ValidationResult results = userloginvalidation.Validate(userLogin);
                User user = oauth.User.Where(x => x.UserName == userLogin.UserName).FirstOrDefault();
                UserDTO userLoginDTO = mapper.Map<UserDTO>(user);
                userLoginDTO.password = userLogin.password;
                var handler = new UserAuthenticationHandler();
                handler.Handle(userLoginDTO);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userLoginDTO);
                auth.refresh_token = GetRefreshToken(userLoginDTO.UserName);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }
 

        private string GetRefreshToken(string username)
        {
            string refresh_token = JWTTokenService.GenerateRefreshToken();
            User user = oauth.User.SingleOrDefault(x => x.UserName == username);
            user.RefreshToken = refresh_token;
            oauth.SaveChanges();
            return refresh_token;
        }

    }
}

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
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public ITokenService JWTTokenService { get; set; }
        public ITokenServiceDbContext oauth { get; set; }
        public IEncryptionService EncryptSvc { get; set; }
        public string SecretKey { get; set; }


        public AuthenticationService(IConfiguration configuration,ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) :base(log)
        {
            this.config = configuration;
            this.JWTTokenService = jWTTokenService;
            this.oauth = oauth;
            this.EncryptSvc = encryptSvc;
        }

        public AuthenticationDTO AuthenticateUserLogin(UserLoginDTO userLogin)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {                
                ValidationResult results = userloginvalidation.Validate(userLogin);
                User user = oauth.User.Where(x => x.UserName == userLogin .UserName).FirstOrDefault();
                UserLoginDTO userLoginDTO = mapper.Map<UserLoginDTO>(user);
                userLoginDTO.password = userLogin.password;
                var handler = new UserAuthenticationHandler();
                handler.Handle(userLoginDTO);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userLoginDTO);
                auth.refresh_token = GetRefreshToken(userLoginDTO.UserName);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.Message + " " + ex.StackTrace, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }

        public AuthenticationDTO AuthenticateRefreshToken(RefreshTokenDTO refauth)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                User user = oauth.User.Where(x => x.RefreshToken == refauth.Authorization).FirstOrDefault();
                UserLoginDTO userLoginDTO = mapper.Map<UserLoginDTO>(user);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userLoginDTO);
                auth.refresh_token = GetRefreshToken(user.UserName);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.Message+" "+ex.StackTrace, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }

        private string GetRefreshToken(string username)
        {
            string refresh_token = JWTTokenService.GenerateRefreshToken();
            User user = oauth.User.SingleOrDefault(x => x.UserName == username);
            user.RefreshToken = refresh_token;
            //oauth.Update(user);
            oauth.SaveChanges();
            return refresh_token;
        }

    }
}

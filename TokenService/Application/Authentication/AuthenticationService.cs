using Application.Authentication.Handlers;
using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Application
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public ITokenService JWTTokenService { get; set; }
        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }
        public string SecretKey { get; set; }

        public AuthenticationService()
        {
        }

        public AuthenticationService(IConfiguration configuration)
        {
            config = configuration;
            EncryptSvc = new EncryptionService();
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(EncryptSvc, configuration);
        }
               
        public AuthenticationDTO Authenticate(UserLoginDTO userLoginDTO)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                UserLogin userLogin = mapper.Map<UserLogin>(userLoginDTO);
                ValidationResult results = userloginvalidation.Validate(userLogin);
                userLoginDTO.users = mapper.Map<Users>(DBService.GetUser(userLogin.username));
                userLoginDTO.encryptionService = new EncryptionService();

                var handler = new UserAuthenticationHandler();
                handler.Handle(userLoginDTO);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GetToken(userLoginDTO.users);
                auth.refresh_token = GetRefreshToken(userLoginDTO.username);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.Message, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }

    
        public AuthenticationDTO RefreshToken(RefreshTokenDTO refauth)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                //ValidationResult results = userloginvalidation.Validate(userLogin);
                Users users = DBService.GetUserFromRefreshToken(refauth.Authorization);           
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GetToken(users);
                auth.refresh_token = GetRefreshToken(users.UserName);
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.Message, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }

        private string GetRefreshToken(string username)
        {
            string refresh_token = JWTTokenService.GenerateRefreshToken();
            DBService.UpdateUserRefreshToken(username, refresh_token);
            return refresh_token;
        }

    }
}

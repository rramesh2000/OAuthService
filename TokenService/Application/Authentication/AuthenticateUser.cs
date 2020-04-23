﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Application.Authentication
{
    public class AuthenticateUser : BaseService, IAuthenticate
    {


        public string SecretKey { get; set; }
        public AuthenticateUser(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }

        public AuthenticationDTO AuthenticateGetToken(AuthorizationGrantRequestDTO authorizationGrantRequest)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                UserLogin userLogin = new UserLogin(config, Log, JWTTokenService, oauth, EncryptSvc);
                UserDTO userDTO = new UserDTO { UserName = authorizationGrantRequest.UserName, password = authorizationGrantRequest.Password, Grant_Type = authorizationGrantRequest.Grant_Type };
                UserDTO userLoginDTO = userLogin.Login(userDTO);
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

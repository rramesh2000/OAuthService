using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Web;

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
                //Authorize authorize = oauth.Authorize.Where(x => x.Code == refauth.Authorization).FirstOrDefault();
                UserLogin userLogin = new UserLogin(config, Log, JWTTokenService, oauth, EncryptSvc);
                UserDTO userDTO = new UserDTO { UserName = authorizationGrantRequest.UserName, password = authorizationGrantRequest.Password, Grant_Type = authorizationGrantRequest.Grant_Type };
                UserDTO userLoginDTO = userLogin.Login(userDTO);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userLoginDTO);
                auth.refresh_token = HttpUtility.UrlEncode(GetRefreshToken(userLoginDTO.UserName));
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }



        private string GetRefreshToken(string Code)
        {
            string refresh_token = JWTTokenService.GenerateRefreshToken();
            Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == Code);
            authorize.Code = refresh_token;
            oauth.SaveChanges();
            return refresh_token;
        }


    }
}

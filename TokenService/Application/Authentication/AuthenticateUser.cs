using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;

namespace Application.Authentication
{
    public class AuthenticateUser : BaseService, IAuthenticate
    {


        public string SecretKey { get; set; }
        public AuthenticateUser(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }

        public AuthenticationDTO AuthenticateGetToken(TokenDTO tokenDTO)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                //Authorize authorize = oauth.Authorize.Where(x => x.Code == refauth.Authorization).FirstOrDefault();
                UserLogin userLogin = new UserLogin(refreshtoken, config, Log, JWTToken, oauth, EncryptSvc);
                UserDTO userDTO = new UserDTO { UserName = tokenDTO.UserName, password = tokenDTO.Password, Grant_Type = tokenDTO.Grant_Type };
                UserDTO userLoginDTO = userLogin.Login(userDTO);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTToken.GenerateToken(tokenDTO);
                auth.refresh_token = HttpUtility.UrlEncode(refreshtoken.GenerateToken(tokenDTO));
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex, TokenConstants.InvalidUser);
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            return auth;
        }



    }
}

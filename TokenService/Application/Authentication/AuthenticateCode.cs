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
    public class AuthenticateCode : BaseService, IAuthenticate
    {
        public string SecretKey { get; set; }

        public AuthenticateCode(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }

        public AuthenticationDTO AuthenticateGetToken(TokenDTO tokenDTO)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                string _code = HttpUtility.UrlDecode(tokenDTO.Code);
                string _refreshToken = refreshtoken.GenerateToken(tokenDTO);

                //ValidationResult results = refreshvalidation.Validate(refauth);
                Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == _code);
                User user = oauth.User.SingleOrDefault(x => x.UserId == authorize.UserId);
                auth.token_type = config["TokenType"];
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                auth.access_token = JWTToken.GenerateToken(tokenDTO);
                auth.refresh_token = HttpUtility.UrlEncode(_refreshToken);
                authorize.Code = _refreshToken;
                oauth.SaveChanges();
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

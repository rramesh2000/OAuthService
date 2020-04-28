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
        RefreshToken refreshtoken { get; set; }

        public AuthenticateCode(RefreshToken refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
            this.refreshtoken = refreshtoken;
            if (this.refreshtoken==null)
            {
                 this.refreshtoken = new RefreshToken(configuration, log, jWTTokenService, oauth, encryptSvc);                     
            }
        }

        public AuthenticationDTO AuthenticateGetToken(AuthorizationGrantRequestDTO authorizationGrantRequest)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                string _code = HttpUtility.UrlDecode(authorizationGrantRequest.Code);
                //ValidationResult results = refreshvalidation.Validate(refauth);
                Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == _code);
                User user = oauth.User.SingleOrDefault(x => x.UserId == authorize.UserId);
                auth.token_type = config["TokenType"];
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                auth.access_token = JWTTokenService.GenerateAccessToken(userDTO);
                auth.refresh_token = HttpUtility.UrlEncode(refreshtoken.GetRefreshToken(_code));
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

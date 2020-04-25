using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Web;

namespace Application.Authentication
{
    public class AuthenticateRefresh : BaseService, IAuthenticate
    {
        public string SecretKey { get; set; }
        public AuthenticateRefresh(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }

        public AuthenticationDTO AuthenticateGetToken(AuthorizationGrantRequestDTO authorizationGrantRequest) 
        {
            RefreshTokenDTO refauth = new RefreshTokenDTO { Authorization= authorizationGrantRequest.Refresh_Token};
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                ValidationResult results = refreshvalidation.Validate(refauth);
                Authorize authorize  = oauth.Authorize.Where(x => x.Code == refauth.Authorization).FirstOrDefault();
                User user = oauth.User.SingleOrDefault(x => x.UserId == authorize.UserId);
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userDTO);
                auth.refresh_token = HttpUtility.UrlEncode(GetRefreshToken(authorize.Code));
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

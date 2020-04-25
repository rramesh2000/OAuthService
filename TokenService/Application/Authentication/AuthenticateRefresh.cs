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
                User user = oauth.User.Where(x => x.RefreshToken == refauth.Authorization).FirstOrDefault();
                UserDTO userLoginDTO = mapper.Map<UserDTO>(user);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userLoginDTO);
                auth.refresh_token = HttpUtility.UrlEncode(GetRefreshToken(user.UserName));
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

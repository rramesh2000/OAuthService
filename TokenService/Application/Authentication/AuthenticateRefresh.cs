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
        RefreshToken refreshtoken { get; set; }
        public AuthenticateRefresh(RefreshToken refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
            this.refreshtoken = refreshtoken;
            if (this.refreshtoken == null)
            {
                this.refreshtoken = new RefreshToken(configuration, log, jWTTokenService, oauth, encryptSvc);
            }
        }

        public AuthenticationDTO AuthenticateGetToken(AuthorizationGrantRequestDTO authorizationGrantRequest)
        {
            string _refreshToken = HttpUtility.UrlDecode(authorizationGrantRequest.Refresh_Token);
            RefreshTokenDTO refauth = new RefreshTokenDTO { Authorization = _refreshToken };
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                ValidationResult results = refreshvalidation.Validate(refauth);
                Authorize authorize = oauth.Authorize.Where(x => x.Code == refauth.Authorization).FirstOrDefault();
                User user = oauth.User.SingleOrDefault(x => x.UserId == authorize.UserId);
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                auth.token_type = config["TokenType"];
                auth.access_token = JWTTokenService.GenerateAccessToken(userDTO);
                auth.refresh_token = HttpUtility.UrlEncode(refreshtoken.GetRefreshToken(authorize.Code));
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

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

        public AuthenticateRefresh(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }

        public AuthenticationDTO AuthenticateGetToken(TokenDTO tokenDTO)
        {
            string _refreshTokenold = HttpUtility.UrlDecode(tokenDTO.Refresh_Token);
            string _refreshToken = refreshtoken.GenerateToken(tokenDTO);
            RefreshTokenDTO refauth = new RefreshTokenDTO { Authorization = _refreshTokenold };
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
              //  ValidationResult results = refreshvalidation.Validate(refauth);
                Authorize authorize = oauth.Authorize.Where(x => x.Code == refauth.Authorization).FirstOrDefault();
                User user = oauth.User.SingleOrDefault(x => x.UserId == authorize.UserId);
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                auth.token_type = config["TokenType"];
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

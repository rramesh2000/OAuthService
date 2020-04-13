using Application;
using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class TokenController : BaseController
    {
        public TokenController(IConfiguration configuration ) : base(configuration)
        {
        }

        [HttpPost]
        [Route("/api/token")]
        public IActionResult Post(UserLoginDTO user)
        {
            AuthenticationDTO Authorization = new AuthenticationDTO();
            try
            {
                IAuthenticationService tm = new AuthenticationService(configuration, itsLogger, JWTTokenService, OAuthDbContext,EncryptionService);
                Authorization = tm.AuthenticateUserLogin(user);
            }
            catch (InvalidUserException exUser)
            {
                return Unauthorized(new UnauthorizedError(exUser.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(new UnauthorizedError(ex.Message));
            }
            return Ok(Authorization);
        }

        [HttpPost]
        [Route("/api/token/verify")]
        [ActionName("verify")]
        public IActionResult Post(AccessTokenDTO auth)
        {
            string tmp = String.Empty;
            try
            {
                TokenValidationService tm = new TokenValidationService(itsLogger, configuration);
                tmp = tm.VerifyToken(auth);
            }
            catch (InvalidTokenException exToken)
            {
                return Unauthorized(new UnauthorizedError(exToken.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(new UnauthorizedError(ex.Message));
            }
            return Ok(tmp);
        }

        [HttpPost]
        [Route("/api/token/refresh")]
        [ActionName("refresh")]
        public IActionResult Post(RefreshTokenDTO auth)
        {
            AuthenticationDTO Authorization = new AuthenticationDTO();
            try
            {
                IAuthenticationService tm = new AuthenticationService(configuration,itsLogger, JWTTokenService, OAuthDbContext, EncryptionService);
                Authorization = tm.AuthenticateRefreshToken(auth);
            }
            catch (InvalidUserException exUser)
            {
                return Unauthorized(new UnauthorizedError(exUser.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(new UnauthorizedError(ex.Message));
            }
            return Ok(Authorization);
        }

    }
}


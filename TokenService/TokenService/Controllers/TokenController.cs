using Application.Authentication;
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
    public class TokenController : ControllerBase
    {
        private IEncryptionService _encryptionService;
        private IConfiguration _configuration;
        private ITokenService _tokenService;
        private ITokenService _refreshService;
        private ITSLogger _tSLogger;
        private ITokenServiceDbContext _tokenServiceDbContext;


        public TokenController(IConfiguration configuration, IEncryptionService encryption, ITokenService tokenService, ITokenService refrshService, ITSLogger tSLogger, ITokenServiceDbContext tokenServiceDbContext)
        {
            _configuration = configuration;
            _encryptionService = encryption;
            _tokenService = tokenService;
            _refreshService = refrshService;
            _tSLogger = tSLogger;
            _tokenServiceDbContext = tokenServiceDbContext;
        }


        [HttpPost]
        [Route("/api/token")]
        public IActionResult Post(AuthorizationGrantRequestDTO token)
        {
            AuthenticationDTO Authorization = new AuthenticationDTO();
            try
            {
                IAuthenticationService tm = new AuthenticationService(
                    _refreshService,
                    _configuration,
                    _tSLogger,
                   _tokenService,
                    _tokenServiceDbContext,
                    _encryptionService);
                Authorization = tm.Authenticate(token);
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
            string tmp = string.Empty;
            try
            {
                TokenValidationService tm = new TokenValidationService(
                   _refreshService,
                    _configuration,
                    _tSLogger,
                   _tokenService,
                    _tokenServiceDbContext,
                    _encryptionService);
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

        //[HttpPost]
        //[Route("/api/token/refresh")]
        //[ActionName("refresh")]
        //public IActionResult Post(RefreshTokenDTO auth)
        //{
        //    AuthenticationDTO Authorization = new AuthenticationDTO();
        //    try
        //    {
        //        IAuthenticationService tm = new AuthenticationService(
        //            configuration,
        //            itsLogger,
        //            JWTTokenService,
        //            OAuthDbContext,
        //            EncryptionService);
        //        Authorization = tm.AuthenticateRefreshToken(auth);
        //    }
        //    catch (InvalidUserException exUser)
        //    {
        //        return Unauthorized(new UnauthorizedError(exUser.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(new UnauthorizedError(ex.Message));
        //    }
        //    return Ok(Authorization);
        //}

    }
}


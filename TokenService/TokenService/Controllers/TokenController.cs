using Application;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.TokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {
        public TokenController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost]
        public IActionResult Post(UserLoginDTO user)
        {
            AuthenticationDTO Authorization = new AuthenticationDTO();
            try
            {
                AuthenticationService tm = new AuthenticationService(configuration);
                Authorization = tm.Authenticate(user);
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
        public IActionResult Post(AccessTokenDTO auth)
        {
            string tmp = String.Empty;
            try
            {
                TokenValidationService tm = new TokenValidationService(configuration);
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
        public IActionResult Post(RefreshTokenDTO auth)
        {
            AuthenticationDTO Authorization = new AuthenticationDTO();
            try
            {
                AuthenticationService tm = new AuthenticationService(configuration);
                Authorization = tm.RefreshToken(auth);
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


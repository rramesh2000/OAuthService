using Application.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthorizeController : BaseController
    {
        public AuthorizeController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet]

        public IActionResult Get([FromQuery] AuthorizationRequestDTO authorizeDTO)
        {
            AuthorizationResponseDTO authResponseDTO = new AuthorizationResponseDTO();
            try
            {
                IAuthorizationService authorizationService = new AuthorizationService(
                    configuration,
                    itsLogger,
                    JWTTokenService,
                    OAuthDbContext,
                    EncryptionService);
                authResponseDTO = authorizationService.GetAuthorization(authorizeDTO);
            }
            catch (InvalidTokenException exToken)
            {
                return Unauthorized(new UnauthorizedError(exToken.Message));
            }
            catch (InvalidUserException exUser)
            {
                return Unauthorized(new UnauthorizedError(exUser.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(new UnauthorizedError(ex.Message));
            }
            return Ok(authResponseDTO);

        }

    }
}
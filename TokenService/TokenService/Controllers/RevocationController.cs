using Application.Authentication;
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
    public class RevocationController : BaseController
    {
        public RevocationController(IConfiguration configuration) : base(configuration)
        {
        }

        // POST: api/Token
        [HttpPost]
        [Route("api/revoke")]
        public IActionResult Post(RevocationDTO revoke)
        {
            string tmp = string.Empty;
            try
            {
                IRevocationService rs = new RevocationService(
                    configuration,
                    itsLogger,
                    JWTTokenService,
                    OAuthDbContext,
                    EncryptionService);
                tmp = rs.TokenRevocation(revoke);
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
            return Ok(tmp);
        }

    }
}
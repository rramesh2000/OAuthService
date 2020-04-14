using Application.Common.Exceptions;
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
        public IActionResult Post(UserDTO user)
        {
            try
            {

            }
            catch (InvalidUserException exUser)
            {
                return Unauthorized(new UnauthorizedError(exUser.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(new UnauthorizedError(ex.Message));
            }
            return Ok( );
        }

    }
}
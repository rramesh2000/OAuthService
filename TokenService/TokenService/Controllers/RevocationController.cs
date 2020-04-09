using Application.Common.Exceptions;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevocationController : ControllerBase
    {

        // POST: api/Token
        [HttpPost]
        public IActionResult Post(UserLoginDTO user)
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
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Registration;
using Microsoft.AspNetCore.Mvc;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        // POST: api/Token
        [HttpPost]
        public IActionResult Post(UserDTO user)
        {
            try
            {
                RegistrationService registrationService = new RegistrationService();
                user = registrationService.SaveUser(user);
            }
            catch (InvalidUserException exUser)
            {
                return BadRequest(new BadRequestError(exUser.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestError(ex.Message));
            }
            return Ok(user);
        }


    }
}
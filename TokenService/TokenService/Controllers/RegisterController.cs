﻿using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class RegisterController : BaseController
    {
        public RegisterController(IConfiguration configuration) : base(configuration)
        {
        }

        // POST: api/Token
        [HttpPost]
        [Route("/api/register")]
        public IActionResult Post(UserDTO user)
        {
            try
            {
                RegistrationService registrationService = new RegistrationService(configuration, itsLogger, JWTTokenService, OAuthDbContext, EncryptionService);
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
using Application.Authentication;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
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
    public class RegisterController : ControllerBase
    {
        private IEncryptionService _encryptionService;
        private IConfiguration _configuration;
        private ITokenService _tokenService;
        private ITokenService _refreshService;
        private ITSLogger _tSLogger;
        private ITokenServiceDbContext _tokenServiceDbContext;


        public RegisterController(IConfiguration configuration, IEncryptionService encryption, ITokenService tokenService, ITokenService refrshService, ITSLogger tSLogger, ITokenServiceDbContext tokenServiceDbContext)
        {
            _configuration = configuration;
            _encryptionService = encryption;
            _tokenService = tokenService;
            _refreshService = refrshService;
            _tSLogger = tSLogger;
            _tokenServiceDbContext = tokenServiceDbContext;
        }


        // POST: api/Token
        [HttpPost]
        [Route("/api/register/user")]
        public IActionResult Post(UserDTO user)
        {
            try
            {
                RegistrationService registrationService = new RegistrationService(
                      _refreshService,
                    _configuration,
                    _tSLogger,
                   _tokenService,
                    _tokenServiceDbContext,
                    _encryptionService);
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

        // POST: api/Token
        [HttpPost]
        [Route("/api/register/client")]
        public IActionResult Post(ClientDTO client)
        {
            try
            {
                RegistrationService registrationService = new RegistrationService(
                      _refreshService,
                    _configuration,
                    _tSLogger,
                   _tokenService,
                    _tokenServiceDbContext,
                    _encryptionService);
                client = registrationService.SaveClient(client);
            }
            catch (InvalidUserException exUser)
            {
                return BadRequest(new BadRequestError(exUser.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestError(ex.Message));
            }
            return Ok(client);
        }
    }
}
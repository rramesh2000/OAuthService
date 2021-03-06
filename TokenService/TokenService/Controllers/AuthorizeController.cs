﻿using Application.Authorization;
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
    public class AuthorizeController : ControllerBase
    {
        private IEncryptionService _encryptionService;
        private IConfiguration _configuration;
        private ITokenService _tokenService;
        private ITokenService _refreshService;
        private ITSLogger _tSLogger;
        private ITokenServiceDbContext _tokenServiceDbContext;


        public AuthorizeController(IConfiguration configuration, IEncryptionService encryption, ITokenService tokenService, ITokenService refrshService, ITSLogger tSLogger, ITokenServiceDbContext tokenServiceDbContext)
        {
            _configuration = configuration;
            _encryptionService = encryption;
            _tokenService = tokenService;
            _refreshService = refrshService;
            _tSLogger = tSLogger;
            _tokenServiceDbContext = tokenServiceDbContext;
        }

        [HttpGet]
        [Route("/api/authorize")]
        public IActionResult Get([FromQuery] AuthorizationRequestDTO authorizeDTO)
        {
            AuthorizationResponseDTO authResponseDTO = new AuthorizationResponseDTO();
            try
            {
                IAuthorizationService authorizationService = new AuthorizationService(
                      _refreshService,
                    _configuration,
                    _tSLogger,
                   _tokenService,
                    _tokenServiceDbContext,
                    _encryptionService);
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
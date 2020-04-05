using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokenService.Utility;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        // POST: api/Token
        [HttpPost]
        public IActionResult Post(User user)
        {
            string tmp = "";
            IEncryptionService enscv = new EncryptionService();
            RegistrationService registrationService = new RegistrationService(enscv);
            Response response = registrationService.SaveUser(user);
            if (response.httpstatus != HttpStatusCode.Created)
                return BadRequest(new BadRequestError(response.Body));
            else
                return Ok(tmp);
        }


    }
}
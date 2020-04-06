using System.Net;
using Application;
using Application.Registration;
using Domain;
using Domain.Common;
using Domain.Entities;
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
            RegistrationService registrationService = new RegistrationService();
            Response response = registrationService.SaveUser(user);
            if (response.httpstatus != HttpStatusCode.Created)
                return BadRequest(new BadRequestError(response.Body));
            else
                return Ok(tmp);
        }


    }
}
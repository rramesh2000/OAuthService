using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        // POST: api/Token
        [HttpPost]
        public string Post(User user)
        {
            string result = String.Empty;
            try
            {
                IEncryptionService enscv = new EncryptionService();
                RegistrationService registrationService = new RegistrationService(enscv);
                if (registrationService.SaveUser(user))
                {
                    result = "Saved User";
                }                                       
            }
            catch (Exception ex)
            {
                result = "Errorsaving User";
            }
            return result;
        }


    }
}
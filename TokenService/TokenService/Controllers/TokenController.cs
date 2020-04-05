using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {       

        public TokenController(IConfiguration config)
        {
         
        }

        // POST: api/Token
        [HttpPost]
        public IActionResult Post(UserLogin user)
        {
            string Authorization = null;
            try
            {              
                AuthenticationService tm = new AuthenticationService(base.Configuration["Secretkey"]);
                Authorization = tm.Authenticate(user);
            }
            catch (Exception ex)
            {
                //TODO: Genrate approporiate error 
            }
            return Ok(Authorization);
        }


        [HttpPost]
        [Route("/api/token/verify")]
        public IActionResult Post(AccessToken auth)
        {
            string tmp = "Invalid token";
            try
            {                
                TokenValidationService tm = new TokenValidationService(base.Configuration["Secretkey"]);
                if (tm.VerifyToken(auth.Authorization))
                {
                    tmp = "Valid token";                
                }
            }
            catch (Exception ex)
            {
                //TODO: Genrate approporiate error 
            }
            return Ok(tmp);
        }
 

 
    }
}


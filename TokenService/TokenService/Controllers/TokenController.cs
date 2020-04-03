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

        // GET: api/Token
        [HttpGet]
        public string Get()
        {
            string Authorization = null;
            try
            {
            
            }
            catch (Exception ex)
            { 
           
            }
            return Authorization;
        }

        // POST: api/Token
        [HttpPost]
        public string Post(UserLogin user)
        {
            string Authorization = null;
            try
            {
                IEncryptionService enscv = new EncryptionService();
                AuthenticationService tm = new AuthenticationService(enscv, base.Configuration["Secretkey"]);
                Authorization = tm.Authenticate(user);
            }
            catch (Exception ex)
            {

            }
            return Authorization;
        }


        [HttpPost]
        [Route("/api/token/verify")]
        public string Post(AccessToken auth)
        {
            string tmp = "Invalid token";
            try
            {
                IEncryptionService enscv = new EncryptionService();
                TokenValidationService tm = new TokenValidationService(enscv, base.Configuration["Secretkey"]);
                if (tm.VerifyToken(auth.Authorization))
                {
                    tmp = "Valid token";                
                }
            }
            catch (Exception ex)
            {

            }
            return tmp;
        }

        // PUT: api/Token/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


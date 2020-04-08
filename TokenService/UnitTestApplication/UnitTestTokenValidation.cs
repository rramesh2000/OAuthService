using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application;
using Domain;
using Application.Common.Behaviours;
using Application.TokenValidation;
using Microsoft.Extensions.Configuration;
using Application.Common.Models;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestTokenValidation
    {
        public UnitTestTokenValidation()
        {
            IConfiguration configuration = null;
            Secret = "56345555555466666666666666666666758678679789780890956757";
            encryptionService = new EncryptionService();
            tokenValidationService = new TokenValidationService(encryptionService, configuration);
        }

        public string Secret { get; set; }

        public EncryptionService encryptionService { get; set; }
        public TokenValidationService tokenValidationService   { get; set; }

        [TestMethod]
        public void TestMethodVerifyToken()
        {
            AccessTokenDTO acc = new AccessTokenDTO { Authorization = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IlJpdGVzaCIsImFkbWluIjp0cnVlfQ==.5cocLLBW3HrXvgeQMzKiI5cDWWiUYcUA674BwWwxJM0=" };
            Assert.IsTrue(tokenValidationService.VerifyToken(acc)== "Valid Token");
        }

         
    }
}
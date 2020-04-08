using Application.Common.Behaviours;
using Application.Common.Models;
using Application.TokenValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestTokenValidation
    {
        public EncryptionService encryptionService { get; set; }
        public TokenValidationService tokenValidationService { get; set; }

        public UnitTestTokenValidation()
        {
            var myConfiguration = new Dictionary<string, string> { { "Secretkey", "56345555555466666666666666666666758678679789780890956757" } };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
            encryptionService = new EncryptionService();
            tokenValidationService = new TokenValidationService(encryptionService, configuration);
        }

        [TestMethod]
        public void TestMethodVerifyToken()
        {          
            AccessTokenDTO acc = new AccessTokenDTO { Authorization = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IlJpdGVzaCIsImFkbWluIjp0cnVlfQ==.5cocLLBW3HrXvgeQMzKiI5cDWWiUYcUA674BwWwxJM0=" };
            Assert.IsTrue(tokenValidationService.VerifyToken(acc) == "Valid Token");
        }


    }
}
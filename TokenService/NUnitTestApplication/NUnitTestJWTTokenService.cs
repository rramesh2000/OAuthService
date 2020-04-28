using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.JWT;
using Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;

namespace NUnitTestApplication
{
    public class NUnitTestJWTTokenService
    {
        private IConfiguration configuration;
        private IEncryptionService encryptSvc;
        private ITokenService jWTToken;
        private ITSLogger itsLogger { get; set; }

        [SetUp]
        public void Setup()
        {
            itsLogger = new TSLogger();
            encryptSvc = new EncryptionService();
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            jWTToken = new JWTToken(itsLogger, encryptSvc, configuration);
        }

        [Test]
        public void TestVerifyToken()
        {
            TokenDTO tokenDTO = new TokenDTO { UserName = "rramesh" };            
            Assert.IsTrue(jWTToken.VerifyToken(jWTToken.GenerateToken(tokenDTO)));
        }

        [Test]
        public void TestVerifyTokenTime()
        {
            TokenDTO tokenDTO = new TokenDTO { UserName = "rramesh" };
            Assert.IsTrue(jWTToken.VerifyTokenTime(jWTToken.GenerateToken(tokenDTO)));
        }

    }
}

using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;

namespace NUnitTestApplication
{
    public class NUnitTestJWTTokenService
    {
        private IConfiguration configuration;
        private IEncryptionService encryptSvc;
        private JWTTokenService jWTTokenService;

        [SetUp]
        public void Setup()
        {
            encryptSvc = new EncryptionService();
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            jWTTokenService = new JWTTokenService(encryptSvc, configuration);
        }

        [Test]
        public void TestVerifyToken()
        {
            Users use = new Users { UserName="rramesh", Salt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=", HashPassword="", UserId= Guid.NewGuid() };
            Assert.IsTrue(jWTTokenService.VerifyToken(jWTTokenService.GetToken(use)));
        }

        [Test]
        public void TestVerifyTokenTime()
        {
            Users use = new Users { UserName = "rramesh", Salt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=", HashPassword = "", UserId = Guid.NewGuid() };
            Assert.IsTrue(jWTTokenService.VerifyTokenTime(jWTTokenService.GetToken(use)));
        }

    }
}

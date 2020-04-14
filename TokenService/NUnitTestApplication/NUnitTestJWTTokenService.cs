using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
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
        private ITokenService jWTTokenService;
        private ITSLogger itsLogger { get; set; }

        [SetUp]
        public void Setup()
        {
            itsLogger = new TSLogger();
            encryptSvc = new EncryptionService();
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            jWTTokenService = new JWTTokenService(itsLogger,encryptSvc, configuration);
        }

        [Test]        
        public void TestVerifyToken()
        {
            UserDTO use = new UserDTO { UserName = "rramesh", Salt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=", HashPassword = "", UserId = Guid.NewGuid() };
            Assert.IsTrue(jWTTokenService.VerifyAccessToken(jWTTokenService.GenerateAccessToken(use)));
        }

        [Test]
        public void TestVerifyTokenTime()
        {
            UserDTO use = new UserDTO { UserName = "rramesh", Salt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=", HashPassword = "", UserId = Guid.NewGuid() };
            Assert.IsTrue(jWTTokenService.VerifyAccessTokenTime(jWTTokenService.GenerateAccessToken(use)));
        }

    }
}

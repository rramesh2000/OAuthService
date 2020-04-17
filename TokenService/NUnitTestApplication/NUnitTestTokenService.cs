using Application;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.JWT;
using Application.TokenValidation;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;

namespace NUnitTestApplication
{
    public class NUnitTestTokenService
    {
        private IConfiguration configuration;
        private Infrastructure.Models.OAuthContext context { get; set; }

        [SetUp]
        public void Setup()
        {
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            var options = new DbContextOptionsBuilder<Infrastructure.Models.OAuthContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new Infrastructure.Models.OAuthContext(options);
            context.Database.EnsureCreated();
            context.User.Add(new User { UserId = Guid.NewGuid(), UserName = "ronald", Salt = "pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw=", HashPassword = "iO27OOseTcQsdEbch+UgOqRWGy80o7aZNit80EwggYgJ4f3vfFkMxmk5DKn6OooyiEEUY+YW5pr/9utIb6OR3Z7cFc40ikafRrhQ3sHE1OCM83C4Wxjcffwf721gKLCfJ/vF9AB4KWp/KasDBztCExyAmarZnoKlehZ60iMAlEK/Kgx3J4VualLR+X1gk1bpdP/jNZ1ZVFgxq6b5RR6zeJ1Lf5E+BV2ntO2yv7/67/FdXnqL1kivcoxGxX05TgPd8pSQnVc/As+8S5bQXWmandFkJatkGWQc70edq1qoF80KbARqMWXWJvd2tt+ZfytPuuFga7XU5suwMhTb3s9MUw==" });
            context.SaveChanges();
        }

        [Test]
        public void TestVerifyToken()
        {
            ITSLogger log = new TSLogger();
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            UserDTO userLoginDTO = new UserDTO { UserName = "ronald", password = "test26832549658" };
            IAuthenticationService tm = new AuthenticationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            accessDTO.Authorization = tm.AuthenticateUserLogin(userLoginDTO).access_token;
            TokenValidationService tv = new TokenValidationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            string result = tv.VerifyToken(accessDTO);
            Assert.AreEqual(result, TokenConstants.ValidToken);
        }
    }
}
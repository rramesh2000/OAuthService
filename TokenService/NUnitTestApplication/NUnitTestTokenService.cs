using Application;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TokenValidation;
using Domain.Entities;
using Domain.Enums;
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

            context.User.Add(new User { UserId = Guid.NewGuid(), UserName = "rramesh", HashPassword = "", Salt = "", RefreshToken = "" });
            context.SaveChanges();

        }

        [Test]
        public void TestVerifyToken()
        {
            //AccessTokenDTO accessDTO = new AccessTokenDTO();
            //UserLoginDTO userLoginDTO = new UserLoginDTO { username = "rramesh", password = "Test12345677" };
            //IAuthenticationService tm = new AuthenticationService(new JWTTokenService(new EncryptionService(), configuration), context, new EncryptionService());
            //accessDTO.Authorization = tm.AuthenticateUserLogin(userLoginDTO).access_token;
            //TokenValidationService tv = new TokenValidationService(configuration);
            //string result = tv.VerifyToken(accessDTO);
            //Assert.AreEqual(result, TokenConstants.ValidToken);
        }


    }
}
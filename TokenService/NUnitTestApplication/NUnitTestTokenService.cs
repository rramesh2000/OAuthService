using Application.Authentication;
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
using System.Web;

namespace NUnitTestApplication
{
    public class NUnitTestTokenService
    {
        private IConfiguration configuration;
        private ITokenServiceDbContext context { get; set; }

        [SetUp]
        public void Setup()
        {
            Guid userId = Guid.Parse("71264886-e911-4f71-a9f7-e850967122fd");
            Guid clientId = Guid.Parse("29bfd4b1-81c0-4db3-a615-4422d08f9792");
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            var options = new DbContextOptionsBuilder<Infrastructure.Models.OAuthContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new Infrastructure.Models.OAuthContext(options);
            context.User.Add(new User { UserId = userId, UserName = "ronald", Salt = "pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw=", HashPassword = "iO27OOseTcQsdEbch+UgOqRWGy80o7aZNit80EwggYgJ4f3vfFkMxmk5DKn6OooyiEEUY+YW5pr/9utIb6OR3Z7cFc40ikafRrhQ3sHE1OCM83C4Wxjcffwf721gKLCfJ/vF9AB4KWp/KasDBztCExyAmarZnoKlehZ60iMAlEK/Kgx3J4VualLR+X1gk1bpdP/jNZ1ZVFgxq6b5RR6zeJ1Lf5E+BV2ntO2yv7/67/FdXnqL1kivcoxGxX05TgPd8pSQnVc/As+8S5bQXWmandFkJatkGWQc70edq1qoF80KbARqMWXWJvd2tt+ZfytPuuFga7XU5suwMhTb3s9MUw==" });
            context.Authorize.Add(new Authorize { Client_Id = clientId, Code = "pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw=", UserId = userId, Scope = "photos", ID = 0 });
            context.SaveChanges();
        }

        [Test]
        public void TestVerifyToken()
        {
            ITSLogger log = new TSLogger();
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            AuthorizationGrantRequestDTO authorizationGrantRequestDTO = new AuthorizationGrantRequestDTO
            {
                Client_Id = Guid.Parse("29bfd4b1-81c0-4db3-a615-4422d08f9792"),
                Code = HttpUtility.UrlEncode("pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw="),
                Grant_Type = AuthorizationGrantType.authorization_code,
                UserName = null,
                Scope = null,
                Password = null,
                Redirect_Uri = null,
                Refresh_Token = null,
                State = null
            };
            IAuthenticationService tm = new AuthenticationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            accessDTO.Authorization = accessDTO.Authorization = tm.Authenticate(authorizationGrantRequestDTO).access_token;
            TokenValidationService tv = new TokenValidationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            string result = tv.VerifyToken(accessDTO);
            Assert.AreEqual(result, TokenConstants.ValidToken);
        }
    }
}
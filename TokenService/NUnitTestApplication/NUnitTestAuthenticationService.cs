using Application;
using Application.Authentication;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.JWT;
using Application.TokenValidation;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;

namespace NUnitTestApplication
{
    internal class NUnitTestAuthenticationService
    {
        private IConfiguration configuration;
        private ITokenServiceDbContext context { get; set; }

        [SetUp]
        public void Setup()
        {
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
            context = TestHelper.GetTokenServiceDbContext();
            context.User.Add(new User { UserId = Guid.NewGuid(), UserName = "ronald", Salt = "pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw=", HashPassword = "iO27OOseTcQsdEbch+UgOqRWGy80o7aZNit80EwggYgJ4f3vfFkMxmk5DKn6OooyiEEUY+YW5pr/9utIb6OR3Z7cFc40ikafRrhQ3sHE1OCM83C4Wxjcffwf721gKLCfJ/vF9AB4KWp/KasDBztCExyAmarZnoKlehZ60iMAlEK/Kgx3J4VualLR+X1gk1bpdP/jNZ1ZVFgxq6b5RR6zeJ1Lf5E+BV2ntO2yv7/67/FdXnqL1kivcoxGxX05TgPd8pSQnVc/As+8S5bQXWmandFkJatkGWQc70edq1qoF80KbARqMWXWJvd2tt+ZfytPuuFga7XU5suwMhTb3s9MUw==" });
            context.SaveChanges();
        }

        [Test]
        public void TestAuthenticateUserLogin()
        {
            ITSLogger log = new TSLogger();
            UserDTO userDTO = new UserDTO { UserName = "ronald", password = "test26832549658" };
            UserLogin userLogin = new UserLogin(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            userDTO = userLogin.Login(userDTO);

 
           // Assert.AreEqual(TokenConstants.ValidToken, tv.VerifyToken(accessDTO));
        }

        [Test]
        public void TestAuthenticateRefreshToken()
        {
            ITSLogger log = new TSLogger();
            UserDTO userLogin = new UserDTO { UserName = "ronald", password = "test26832549658" };
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO();
            AuthenticationDTO authenticationDTO = new AuthenticationDTO();
            AuthenticationService authenticationService = new AuthenticationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            authenticationDTO = authenticationService.AuthenticateUserLogin(userLogin);
            refreshTokenDTO.Authorization = authenticationDTO.refresh_token;
            authenticationDTO = authenticationService.AuthenticateRefreshToken(refreshTokenDTO);
            accessDTO.Authorization = authenticationDTO.access_token;
            TokenValidationService tv = new TokenValidationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            Assert.AreEqual(TokenConstants.ValidToken, tv.VerifyToken(accessDTO));
        }

        [Test]
        public void TestAuthenticateRefreshTokenCreatesNewAccessToken()
        {
            ITSLogger log = new TSLogger();
            UserDTO userLogin = new UserDTO { UserName = "ronald", password = "test26832549658" };
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO();
            AuthenticationDTO authenticationDTO = new AuthenticationDTO();
            AuthenticationService authenticationService = new AuthenticationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            authenticationDTO = authenticationService.AuthenticateUserLogin(userLogin);
            refreshTokenDTO.Authorization = authenticationDTO.refresh_token;
            authenticationDTO = authenticationService.AuthenticateRefreshToken(refreshTokenDTO);
            string accessToken1 = authenticationDTO.access_token;
            refreshTokenDTO.Authorization = authenticationDTO.refresh_token;
            authenticationDTO = authenticationService.AuthenticateRefreshToken(refreshTokenDTO);
            string accessToken2 = authenticationDTO.access_token;
            Assert.AreNotEqual(accessToken1, accessToken2);
        }

        [Test]
        public void TestAuthenticateRefreshTokenCreatesNewRefreshToken()
        {
            ITSLogger log = new TSLogger();
            UserDTO userLogin = new UserDTO { UserName = "ronald", password = "test26832549658" };
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO();
            AuthenticationDTO authenticationDTO = new AuthenticationDTO();
            AuthenticationService authenticationService = new AuthenticationService(configuration, log, new JWTTokenService(log, new EncryptionService(), configuration), context, new EncryptionService());
            authenticationDTO = authenticationService.AuthenticateUserLogin(userLogin);
            refreshTokenDTO.Authorization = authenticationDTO.refresh_token;
            authenticationDTO = authenticationService.AuthenticateRefreshToken(refreshTokenDTO);
            string refreshToken1 = authenticationDTO.refresh_token;
            refreshTokenDTO.Authorization = authenticationDTO.refresh_token;
            authenticationDTO = authenticationService.AuthenticateRefreshToken(refreshTokenDTO);
            string refreshToken2 = authenticationDTO.refresh_token;
            Assert.AreNotEqual(refreshToken1, refreshToken2);
        }
    }
}

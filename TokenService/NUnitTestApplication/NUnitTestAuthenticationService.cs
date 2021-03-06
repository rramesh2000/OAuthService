﻿using Application.Authentication;
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
using RefreshToken = Application.Authentication.RefreshToken;

namespace NUnitTestApplication
{
    internal class NUnitTestAuthenticationService
    {
        private IConfiguration configuration;
        private ITokenServiceDbContext context { get; set; }
        private ITokenService refreshtoken { get; set; }

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
        public void TestAuthenticateUserLogin()
        {
            ITSLogger log = new TSLogger();
            UserDTO userDTO = new UserDTO { UserName = "ronald", password = "test26832549658" };
            UserLogin userLogin = new UserLogin(new RefreshToken(), configuration, log, new JWTToken(log, new EncryptionService(), configuration), context, new EncryptionService());
            userDTO = userLogin.Login(userDTO);
            Assert.AreEqual(userDTO.HashPassword, "iO27OOseTcQsdEbch+UgOqRWGy80o7aZNit80EwggYgJ4f3vfFkMxmk5DKn6OooyiEEUY+YW5pr/9utIb6OR3Z7cFc40ikafRrhQ3sHE1OCM83C4Wxjcffwf721gKLCfJ/vF9AB4KWp/KasDBztCExyAmarZnoKlehZ60iMAlEK/Kgx3J4VualLR+X1gk1bpdP/jNZ1ZVFgxq6b5RR6zeJ1Lf5E+BV2ntO2yv7/67/FdXnqL1kivcoxGxX05TgPd8pSQnVc/As+8S5bQXWmandFkJatkGWQc70edq1qoF80KbARqMWXWJvd2tt+ZfytPuuFga7XU5suwMhTb3s9MUw==");
        }

        [Test]
        public void TestAuthenticateAccessTokenFromCode()
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

            IAuthenticationService tm = new AuthenticationService(new RefreshToken(), configuration, log, new JWTToken(log, new EncryptionService(), configuration), context, new EncryptionService());
            accessDTO.Authorization = accessDTO.Authorization = tm.Authenticate(authorizationGrantRequestDTO).access_token;
            TokenValidationService tokenValidationService = new TokenValidationService(new RefreshToken(), configuration, log, new JWTToken(log, new EncryptionService(), configuration), context, new EncryptionService());
            Assert.AreEqual(TokenConstants.ValidToken, tokenValidationService.VerifyToken(accessDTO));
        }

        [Test]
        public void TestAuthenticateAccessTokenFromRefreshToken()
        {
            ITSLogger log = new TSLogger();
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            AuthorizationGrantRequestDTO authorizationGrantRequestDTO = new AuthorizationGrantRequestDTO
            {
                Client_Id = Guid.Parse("29bfd4b1-81c0-4db3-a615-4422d08f9792"),
                Code = null,
                Grant_Type = AuthorizationGrantType.refresh_token,
                UserName = null,
                Scope = null,
                Password = null,
                Redirect_Uri = null,
                Refresh_Token = HttpUtility.UrlEncode("pgsoAvSXD3xYPV+/pSAe3khYZWOFidHPxpltwNDP4Xw="),
                State = null
            };
            IAuthenticationService tm = new AuthenticationService(new RefreshToken(), configuration, log, new JWTToken(log, new EncryptionService(), configuration), context, new EncryptionService());
            accessDTO.Authorization = accessDTO.Authorization = tm.Authenticate(authorizationGrantRequestDTO).access_token;
            TokenValidationService tokenValidationService = new TokenValidationService(new RefreshToken(), configuration, log, new JWTToken(log, new EncryptionService(), configuration), context, new EncryptionService());
            Assert.AreEqual(TokenConstants.ValidToken, tokenValidationService.VerifyToken(accessDTO));
        }
    }
}

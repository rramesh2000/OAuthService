using Application;
using Application.Common.Models;
using Application.TokenValidation;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NUnitTestApplication
{
    public class NUnitTestTokenService
    {
        private IConfiguration configuration;

        [SetUp]
        public void Setup()
        {
            configuration = TestHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
        }

        [Test]
        public void TestVerifyToken()
        {
            AccessTokenDTO accessDTO = new AccessTokenDTO();
            UserLoginDTO userLoginDTO = new UserLoginDTO { username = "rramesh", password = "Test12345677" };
            AuthenticationService auth = new AuthenticationService(configuration);
            accessDTO.Authorization = auth.Authenticate(userLoginDTO).access_token;           
            TokenValidationService tv = new TokenValidationService(configuration);
            string result = tv.VerifyToken(accessDTO);
            Assert.AreEqual(result, TokenConstants.ValidToken);
        }


    }
}
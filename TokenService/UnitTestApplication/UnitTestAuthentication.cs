using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application;
using Domain;
using Infrastructure.Models;
using Domain.Entities;
using Application.Common.Behaviours;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestAuthentication
    {
        public UnitTestAuthentication()
        {
            Secret = "56345555555466666666666666666666758678679789780890956757";
            encryptionService = new EncryptionService();
            authenticationService = new AuthenticationService(encryptionService, Secret);
        }

        public string Secret { get; set; }
   
        public EncryptionService encryptionService { get; set; }
        public AuthenticationService authenticationService { get; set; }

        
    }
}

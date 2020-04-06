using Application;
using Application.Common.Behaviours;
using Application.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestApplication
{    
    [TestClass]
      public  class UnitRegistrationValidation
    { 
        public UnitRegistrationValidation()
        {
            Secret = "56345555555466666666666666666666758678679789780890956757";
            encryptionService = new EncryptionService();
            registrationService = new RegistrationService();
        }

        public string Secret { get; set; }

        public EncryptionService encryptionService { get; set; }
        public RegistrationService registrationService  { get; set; }

        [TestMethod]
        public void TestMethodSaveUser()
        {
            
        }

    }
}

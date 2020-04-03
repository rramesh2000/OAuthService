using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application;
using Domain;
using Infrastructure.Models;

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

        [TestMethod]
        public void TestMethodCheckUser()
        {
            Users user = new Users { UserName = "rramesh", Salt= "OllUgvTiRWieVEJv6XZ9lVzj1I0jWbqQ5jj72BiTfTk=", HashPassword= "CtWLxm8isfNeXSwmB1nRZGnaZN4aI70XlBWwDY59CQiWcuj9macDCrwNieePm8fh37Sl1qqj64UX04/KTg0iPU3J1COADWrvZAzTzlfL8/ElKk3SIspuRLclp9n3vVpfc6gSq4/5LpHpSIWnVsyA8wiacreWTqF+zehhzxKaQj/jj0F/gDR0Xzi0aZcBgdEGr8FIbxKqBQtihgxMWEwvdU0O8UIpLQPam/d3ZWk3uwgfgtiE6v5ak7tn1R35VjTr1wWsB8hx+c4rqYCUzRtAGQKY//Eq/q1X2RHGEiwWW9XxPjfyO4+gha5FzKfAuGg7tJxlTqgEqhSru+1g/c5Z8g==" }; 
            UserLogin _user = new UserLogin { username = "rramesh", password = "Test12345677" };
            Assert.IsTrue(authenticationService.CheckIsUser(_user, user));
        }
    }
}

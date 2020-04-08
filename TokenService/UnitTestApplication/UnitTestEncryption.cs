using Application;
using Application.Common.Behaviours;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestEncryption
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public string Secret { get; set; }
        public EncryptionService encryptionService { get; set; }
        public UnitTestEncryption()
        {
            Secret = "56345555555466666666666666666666758678679789780890956757";
            EncryptionService encryptionService = new EncryptionService();
        }

        [TestMethod]
        public void TestMethodEncrypt()
        {
            Trace.Write("This is a test");
            var obj1 = encryptionService.Encrypt("Testdata123", Secret);
            var obj2 = encryptionService.Encrypt("Testdata321", Secret);
            Trace.Write("code   "+ obj1.GetHashCode().ToString() + "        " + obj2.GetHashCode().ToString());
            //Console.WriteLine(obj1.GetHashCode() + "        "+ obj2.GetHashCode());
            Assert.IsTrue(obj1.GetHashCode() == obj2.GetHashCode());
        }


        //public HashSalt GenerateSaltedHash(string salt, string password)
        //{
        //    var saltBytes = Encoding.UTF8.GetBytes(salt);
        //    var provider = new RNGCryptoServiceProvider();
        //    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        //    var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        //    HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
        //    return hashSalt;
        //}

        //public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        //{
        //    var saltBytes = Encoding.UTF8.GetBytes(storedSalt);
        //    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
        //    return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        //}

        //public string GetSalt()
        //{
        //    var random = new RNGCryptoServiceProvider();
        //    int max_length = 32;
        //    byte[] salt = new byte[max_length];
        //    random.GetNonZeroBytes(salt);
        //    return Convert.ToBase64String(salt);
        //}

    }
}

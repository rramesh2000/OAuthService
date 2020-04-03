using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestEncryption
    {
        public EncryptionService encryptionService { get; set; }
        public UnitTestEncryption()
        {
            EncryptionService encryptionService = new EncryptionService();
        }

        [TestMethod]
        public void TestMethodEncrypt()
        {

            var obj1 = encryptionService.Encrypt("Testdata123", "huowrebcpyjfljghflwrtwrjtwpotmpwotui3tpeeensnsnsns2oa3ebau13b4e297");
            var obj2 = encryptionService.Encrypt("Testdata321", "huowrebcpyjfljghflwrtwrjtwpotmpwotui3tpeeensnsnsns2oa3ebau13b4e297");
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

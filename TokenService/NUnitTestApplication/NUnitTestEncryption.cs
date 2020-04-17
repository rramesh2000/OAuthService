using Application.Common.Behaviours;
using Domain.ValueObjects;
using NUnit.Framework;
using System.Text;

namespace NUnitTestApplication
{
    internal class NUnitTestEncryption
    {
        private EncryptionService encryptionService;
        private string storedSalt = "";
        private string enteredPassword = "";
        private string storedHash = "";
        private string HMACkey = "";
        private string HMACdata = "";
        private byte[] bdata = null;
        private byte[] bkey = null;

        [SetUp]
        public void Setup()
        {
            encryptionService = new EncryptionService();
            storedSalt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=";
            enteredPassword = "Test12345677";
            storedHash = "cWvYeJGu1U5iCKfVOx4TSveeC+rCspHX1te4Ot2IweMb5LJfAqDboMiXReYrjw5OHuD4wio9ixWgpuoGkPNSzztQta53s8uwSmor7CtsWdmscpmrgPHb0fzl8q3TxCjytCPItxLJJ+3/PK/GUBg6IQRfxFL+or5RgGQQQG6+9xxseduXXbE5c2nzZEyDIDbM6Paatta+LpozTZ17mqgVHzAd4+IHSG2kTJyTiCklxdOMGwGQw03GVsAE6kuqJE29/uG0/ZpSolBgtQAhtZuM51vd6JPbCqGYiJCHlZyoBDnMME/GOEZRTBqQzUIQ4vCwDRPbia8J5nTJG+uCqZs88Q==";
            HMACkey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            HMACdata = "This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm.";
            bdata = Encoding.UTF8.GetBytes(HMACdata);
            bkey = Encoding.UTF8.GetBytes(HMACkey);
        }

        [Test]
        public void TestVerifyPassword()
        {
            Assert.IsTrue(encryptionService.VerifyPassword(enteredPassword, storedHash, storedSalt));
        }

        [Test]
        public void TestGetSalt()
        {
            string tmp = encryptionService.GetSalt();
            Assert.IsTrue(tmp.Length > 0);
            string tmp1 = encryptionService.GetSalt();
            Assert.AreNotEqual(tmp, tmp1);
        }

        [Test]
        public void TestGenerateSaltedHash()
        {
            HashSalt hashSalt = encryptionService.GenerateSaltedHashPassword(storedSalt, "Test12345677");
            HashSalt hashSalt1 = encryptionService.GenerateSaltedHashPassword(storedSalt, "Test12345677");
            Assert.AreEqual(hashSalt.Hash.GetHashCode(), hashSalt1.Hash.GetHashCode());
        }

        [Test]
        public void TestComputeHmac256()
        {
            string expected = "2e68979459ebc0e7fcf0f2db3b4aef383835337e271152286e1ea1ad59080a57";       //comparing with result produced at https://codebeautify.org/hmac-generator   
            byte[] bhashStr = encryptionService.ComputeHmacsha256(bdata, bkey);
            string hashStr = ByteArrayToString(bhashStr);
            Assert.AreEqual(expected, hashStr);
        }

        [Test]
        public void TestComputeHmac384()
        {
            string expected = "6f84d499690d0b91bdbef5d8c05b6750cc0590dfc1ab046327c96d3c05cb1fde02017ef673fbd767db8500fad18b1a74";       //comparing with result produced at https://codebeautify.org/hmac-generator   
            byte[] bhashStr = encryptionService.ComputeHmacsha384(bdata, bkey);
            string hashStr = ByteArrayToString(bhashStr);
            Assert.AreEqual(expected, hashStr);
        }

        [Test]
        public void TestComputeHmac512()
        {
            string expected = "ec96b3362e4cfceba4f0b88b4db239a31377d1529d74323844de3aa310e819846c6859f7cce104ce26e62580ae05ae95dcd62d442fc23c2aac1fb32b8332b442";       //comparing with result produced at https://codebeautify.org/hmac-generator   
            byte[] bhashStr = encryptionService.ComputeHmacsha512(bdata, bkey);
            string hashStr = ByteArrayToString(bhashStr);
            Assert.AreEqual(expected, hashStr);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}

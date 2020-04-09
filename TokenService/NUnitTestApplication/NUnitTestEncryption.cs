using Application.Common.Behaviours;
using Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestApplication
{
    class NUnitTestEncryption
    {
        EncryptionService encryptionService;
        string storedSalt = "";
        string enteredPassword = "";
        string storedHash = "";

        [SetUp]
        public void Setup()
        {
            encryptionService = new EncryptionService();
            storedSalt = "z1GRw9XD6tYT10qMqKf0cO7rPcsvkVllugZittGCL0Y=";
            enteredPassword = "Test12345677";
            storedHash = "cWvYeJGu1U5iCKfVOx4TSveeC+rCspHX1te4Ot2IweMb5LJfAqDboMiXReYrjw5OHuD4wio9ixWgpuoGkPNSzztQta53s8uwSmor7CtsWdmscpmrgPHb0fzl8q3TxCjytCPItxLJJ+3/PK/GUBg6IQRfxFL+or5RgGQQQG6+9xxseduXXbE5c2nzZEyDIDbM6Paatta+LpozTZ17mqgVHzAd4+IHSG2kTJyTiCklxdOMGwGQw03GVsAE6kuqJE29/uG0/ZpSolBgtQAhtZuM51vd6JPbCqGYiJCHlZyoBDnMME/GOEZRTBqQzUIQ4vCwDRPbia8J5nTJG+uCqZs88Q==";
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
            HashSalt hashSalt = encryptionService.GenerateSaltedHash(storedSalt, "Test12345677");
            HashSalt hashSalt1 = encryptionService.GenerateSaltedHash(storedSalt, "Test12345678");
            Assert.AreEqual(hashSalt.Hash.Length, hashSalt1.Hash.Length);


        }
    }
}

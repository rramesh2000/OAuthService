using Application.Common.Behaviours.JWT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestJWTExtensions
    {
        [TestMethod]
        public void TestMethodBase64EncodeDecode()
        {
            string str = "Hello this is a test string";
            string data = str.Base64Encode();
            string strResult = data.Base64Decode();
            Assert.IsTrue(str == strResult);
        }

    }
}

using System;
using System.Text;

namespace Application.JWT.Extensions
{
    public static class StringHandler
    {
        public static string Base64Encode(this string tmpStr)
        {
            string base64Encoded;
            byte[] data = Encoding.UTF8.GetBytes(tmpStr);
            base64Encoded = System.Convert.ToBase64String(data);
            return base64Encoded;
        }

        public static string Base64Decode(this string tmpStr)
        {
            string base64Decoded;
            byte[] data = Convert.FromBase64String(tmpStr);
            base64Decoded = Encoding.UTF8.GetString(data);
            return base64Decoded;
        }

        public static string UrlEncode(this string tmpStr)
        {
            return System.Web.HttpUtility.UrlEncode(tmpStr);
        }
    }
}

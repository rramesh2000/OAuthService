using Application.Common.Interfaces;
using Application.Common.Models;
using System;
using System.Security.Cryptography;

namespace Application.Authentication
{
    public class RefreshToken : ITokenService
    {

        public RefreshToken()
        {
        }

        public string GenerateToken(TokenDTO tokenDTO)
        {
            string refresh_token = GenerateRefreshToken();
            //if (tokenDTO.New)
            //{
            //    return refresh_token;
            //}
            //Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == tokenDTO.Code);
            //authorize.Code = refresh_token;
            //oauth.SaveChanges();
            return refresh_token;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public bool VerifyToken(string token)
        {
            bool tmpBool = false;
            //Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == token);
            //if (authorize.Code == token)
            //{
            //    tmpBool = true;
            //}
            return tmpBool;
        }

        public bool VerifyTokenTime(string token)
        {
            bool tmpBool = true;
            return tmpBool;
        }

    }
}

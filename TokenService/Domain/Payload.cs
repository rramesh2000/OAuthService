using System.Collections.Generic;

namespace Domain
{
    public class Payload
    {
        public Payload()
        {
        }

        public Payload(string username, bool admin, string iss, string sub, string aud, string exp, string nbf, string iat, string jti)
        {
            this.username = username;
            this.admin = admin;
            this.iss = iss;
            this.sub = sub;
            this.aud = aud;
            this.exp = exp;
            this.nbf = nbf;
            this.iat = iat;
            this.jti = jti;
        }

        #region Reserved claims
        public string iss { get; set; } //    Issuer of the JWT
        public string sub { get; set; } //Subject of the JWT(the user)
        public string aud { get; set; }// Recipient for which the JWT is intended
        public string exp { get; set; } //Time after which the JWT expires
        public string nbf { get; set; } //Time before which the JWT must not be accepted for processing
        public string iat { get; set; } // Time at which the JWT was issued; can be used to determine age of the JWT
        public string jti { get; set; }// Unique identifier; can be used to prevent the JWT from being replayed(allows a token to be used only once) 
        #endregion


        #region Private claims
        [JWTClaims("private")]
        public string username { get; set; }
        [JWTClaims("private")]
        public bool admin { get; set; } 
        #endregion


    }
}
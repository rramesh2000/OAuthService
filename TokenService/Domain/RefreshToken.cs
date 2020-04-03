using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }

        public RefreshToken(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}

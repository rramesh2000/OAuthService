using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccessToken
    {
        public AccessToken()
        {
        }

        public AccessToken(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}

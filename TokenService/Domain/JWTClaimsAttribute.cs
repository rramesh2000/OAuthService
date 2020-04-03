using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [AttributeUsage(AttributeTargets.All)]
    public class JWTClaimsAttribute :Attribute
    {
        private string claimtype;

        public JWTClaimsAttribute(string claimtype)
        {
            this.claimtype = claimtype;
        }

        public virtual string ClaimType
        {
            get { return claimtype; }
        }
    }
}

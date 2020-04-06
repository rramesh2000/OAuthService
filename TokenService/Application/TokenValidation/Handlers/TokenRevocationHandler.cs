using Application.Common.Behaviours;
using Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TokenValidation.Handlers
{
    public class TokenRevocationHandler: TokenValidationHandler
    {

        public AuthorizationVm _auth { get; set; }
        public override void HandleRequest(AuthorizationVm auth)
        {
            _auth = auth;
            //TODO: Need to impliment this 
            if (0!=0)
            {
                throw new InvalidTokenException("Invalid Token");
            }
            successor.HandleRequest(_auth);
        }
    }
}

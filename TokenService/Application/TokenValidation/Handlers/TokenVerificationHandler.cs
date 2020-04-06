using Application.Common.Behaviours;
using Application.Common.Exceptions;
using System;
namespace Application.TokenValidation.Handlers
{
    public class TokenVerificationHandler : TokenValidationHandler
    {
        public AuthorizationVm _auth { get; set; }
        public override void HandleRequest(AuthorizationVm auth)
        {
            _auth = auth;      
            if (!_auth.JWTTokenService.VerifyToken(_auth.Authorization))
            {
                throw new InvalidTokenException("Invalid Token");
            }
            successor.HandleRequest(_auth);
        }
    }
}

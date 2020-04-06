using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;
namespace Application.TokenValidation.Handlers
{
    public class TokenVerificationHandler : Handler<AuthorizationDTO>
    {
        public override void Handle(AuthorizationDTO auth)
        {
            if (!auth.JWTTokenService.VerifyToken(auth.Authorization))
            {
                throw new InvalidTokenException("Invalid Token");
            }
            base.Handle(auth);
        }

    }
}

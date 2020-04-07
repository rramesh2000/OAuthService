using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;
namespace Application.TokenValidation.Handlers
{
    public class TokenTimeVerificationHandler : Handler<AuthorizationDTO>
    {
        public override void Handle(AuthorizationDTO auth)
        {
            if (!auth.JWTTokenService.VerifyTokenTime(auth.Authorization))
            {
                throw new InvalidTokenException("Invalid Token");
            }
            base.Handle(auth);
        }

    }
}

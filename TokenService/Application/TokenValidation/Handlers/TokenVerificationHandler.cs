using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;
namespace Application.TokenValidation.Handlers
{
    public class TokenVerificationHandler : Handler<ResponseDTO>
    {
        public override void Handle(ResponseDTO auth)
        {
            if (!auth.JWTTokenService.VerifyToken(auth.Authorization))
            {
                throw new InvalidTokenException("Invalid Token");
            }
            base.Handle(auth);
        }

    }
}

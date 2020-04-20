using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;
namespace Application.TokenValidation.Handlers
{
    public class TokenTimeVerificationHandler : Handler<ResponseDTO>
    {
        public override void Handle(ResponseDTO auth)
        {
            if (!auth.JWTTokenService.VerifyAccessTokenTime(auth.Authorization))
            {
                throw new InvalidTokenException("Invalid Token");
            }
            base.Handle(auth);
        }

    }
}

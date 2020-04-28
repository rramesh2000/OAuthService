using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;
using Domain.Enums;

namespace Application.TokenValidation.Handlers
{
    public class TokenRevocationHandler : Handler<ResponseDTO>
    {
        public override void Handle(ResponseDTO auth)
        {
            if (0 != 0)
            {
                throw new InvalidTokenException(TokenConstants.InvalidToken);
            }
            base.Handle(auth);
        }
    }
}

using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.TokenValidation.Models;

namespace Application.TokenValidation.Handlers
{
    public class TokenRevocationHandler : Handler<AuthorizationDTO>
    {
        public override void Handle(AuthorizationDTO auth)
        {
            //TODO: Need to impliment this 
            if (0 != 0)
            {
                throw new InvalidTokenException("Invalid Token");
            }
            base.Handle(auth);
        }
    }
}

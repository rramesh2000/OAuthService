using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Models;
using Domain.Enums;

namespace Application.Authentication.Handlers
{
    public class RefreshTokenAuthenticationHandler : Handler<RevocationDTO>
    {
        public RefreshTokenAuthenticationHandler()
        {
            encryptionService = new EncryptionService();
        }

        public EncryptionService encryptionService { get; private set; }

        public override void Handle(RevocationDTO revoke)
        {
            if (revoke.refresh.Authorization != revoke.user.RefreshToken)
            {
                throw new InvalidTokenException(TokenConstants.InvalidToken);
            }
            base.Handle(revoke);
        }
    }
}

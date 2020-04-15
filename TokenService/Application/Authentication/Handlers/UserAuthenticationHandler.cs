using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Models;
using Domain.Enums;

namespace Application.Authentication.Handlers
{
    public class UserAuthenticationHandler : Handler<UserDTO>
    {
        public UserAuthenticationHandler()
        {
            encryptionService = new EncryptionService();
        }

        public EncryptionService encryptionService { get; private set; }

        public override void Handle(UserDTO userLogin)
        {
            var hash = encryptionService.GenerateSaltedHashPassword(userLogin.Salt, userLogin.password);
            if (!encryptionService.VerifyPassword(userLogin.password, hash.Hash, hash.Salt))
            {
                throw new InvalidUserException(TokenConstants.InvalidUser);
            }
            base.Handle(userLogin);
        }

    }
}

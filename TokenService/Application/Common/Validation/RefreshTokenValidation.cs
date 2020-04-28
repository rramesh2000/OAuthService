using Application.Common.Models;
using FluentValidation;

namespace Application.Common.Validation
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenDTO>
    {
        public RefreshTokenValidation()
        {
            RuleFor(token => token.Authorization).NotEmpty().NotNull();
        }
    }
}

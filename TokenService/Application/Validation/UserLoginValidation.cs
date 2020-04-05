using Domain;
using FluentValidation;

namespace Application.Validation
{
    public class UserLoginValidation : AbstractValidator<UserLogin>
    {
        public UserLoginValidation()
        {
            RuleFor(userlogin => userlogin.username).NotEmpty().NotNull();
            RuleFor(userlogin => userlogin.password).NotEmpty().NotNull();

        }
    }
}

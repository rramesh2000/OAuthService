using Domain;
using FluentValidation;

namespace Application.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(user => user.Username).NotEmpty().NotNull();
            RuleFor(user => user.Password).NotEmpty().NotNull();
        }
    }
}

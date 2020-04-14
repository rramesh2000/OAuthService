using Application.Common.Models;
using Domain;
using Domain.Entities;
using FluentValidation;

namespace Application.Common.Validation
{
    public class UserValidation : AbstractValidator<UserDTO>
    {
        public UserValidation()
        {
            RuleFor(user => user.UserName).NotEmpty().NotNull();
            RuleFor(user => user.password).NotEmpty().NotNull();
        }
    }
}

﻿using Application.Common.Models;
using FluentValidation;

namespace Application.Common.Validation
{
    public class UserLoginValidation : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidation()
        {
            RuleFor(userlogin => userlogin.UserName).NotEmpty().NotNull();
            RuleFor(userlogin => userlogin.password).NotEmpty().NotNull();

        }
    }
}

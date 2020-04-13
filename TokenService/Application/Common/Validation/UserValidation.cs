﻿using Domain;
using Domain.Entities;
using FluentValidation;

namespace Application.Common.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(user => user.UserName).NotEmpty().NotNull();
            //RuleFor(user => user.Password).NotEmpty().NotNull();
        }
    }
}

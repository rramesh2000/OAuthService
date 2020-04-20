﻿using Application.Common.Models;
using Domain;
using Domain.Entities;
using FluentValidation;

namespace Application.Common.Validation
{
    public class ClientValidation : AbstractValidator<ClientDTO>
    {
        public ClientValidation()
        {
            RuleFor(client => client.ClientName).NotEmpty().NotNull();
        }
    }
}

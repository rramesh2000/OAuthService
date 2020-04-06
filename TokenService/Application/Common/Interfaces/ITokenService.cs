﻿using Infrastructure.Models;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GetToken(Users users);
        bool VerifyToken(string token);
    }
}
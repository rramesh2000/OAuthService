﻿using Infrastructure.Models;

namespace Application.Common.Interfaces
{
    public interface IDBService
    {
        OAuthContext oauth { get; set; }
        Users GetUser(string username);
        Users SaveUser(Users user);
    }
}
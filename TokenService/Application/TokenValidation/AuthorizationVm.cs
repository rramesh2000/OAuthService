using Application.Common.Behaviours;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TokenValidation
{
    public class AuthorizationVm
    {
        public AuthorizationVm()
        {
        }

        public AuthorizationVm(string authorization, bool isValid, string secret, ITokenService jWTTokenService)
        {
            Authorization = authorization;
            IsValid = isValid;
            Secret = secret;
            JWTTokenService = jWTTokenService;
        }

        public string Authorization { get; set; }

        public bool IsValid { get; set; }

        public string Secret { get; set; }

        public ITokenService JWTTokenService { get; set; }
    }
}

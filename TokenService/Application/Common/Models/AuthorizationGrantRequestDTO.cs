using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Application.Common.Models
{
    public class AuthorizationGrantRequestDTO
    {
        public AuthorizationGrantType Grant_Type { get; set; }
        public Guid Client_Id { get; set; }    
        public string Redirect_Uri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Refresh_Token { get; set; }

    }

}

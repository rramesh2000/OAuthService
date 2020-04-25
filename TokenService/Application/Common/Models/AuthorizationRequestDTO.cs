using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Application.Common.Models
{
    public class AuthorizationRequestDTO
    {
        [BindNever]
        public int ID { get; set; }
        [FromQuery]
        public AuthorizationGrantType Response_Type { get; set; }
        public Guid Client_Id { get; set; }    
        public string Redirect_Uri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; } 
        public string Client_Secret { get; set; }
        public Guid UserId { get; set; }
    }

}

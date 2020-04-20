using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Application.Common.Models
{
    public class AuthorizeDTO
    {
        [BindNever]
        public int ID { get; set; }
        [FromQuery]
        public AuthorizationGrantType Response_Type { get; set; }
        public Guid Client_Id { get; set; }    
        public string redirect_uri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
    }

}

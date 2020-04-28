using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Application.Common.Models
{
    public class TokenDTO
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public AuthorizationGrantType Grant_Type { get; set; }
        public Guid Client_Id { get; set; }
        public string Redirect_Uri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Refresh_Token { get; set; }
        public bool New { get; set; }

        public TokenDTO()
        {
            New = false;
        }
    }
}

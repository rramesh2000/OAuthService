using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{ 
    public class RefreshTokenDTO
    {
        public RefreshTokenDTO()
        {
        }

        public RefreshTokenDTO(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class AccessTokenDTO    
    {
        public AccessTokenDTO()
        {
        }

        public AccessTokenDTO(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}

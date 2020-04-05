using System;
using System.Collections.Generic;
using System.Text;

namespace TokenService.Utility
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}

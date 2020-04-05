using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain
{
    public class Response
    {
        public HttpStatusCode httpstatus { get; set; }

        public string Header { get; set; }
        public string Body { get; set; }
    }
}

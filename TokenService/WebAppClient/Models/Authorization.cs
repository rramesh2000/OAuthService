using Microsoft.AspNetCore.Mvc;

namespace WebAppClient.Models
{
    public class Authorization
    {
        [BindProperty]
        public string Code { get; set; }
        [BindProperty]
        public string State { get; set; }
        [BindProperty]
        public string access_token { get; set; }
        [BindProperty]
        public string refresh_token { get; set; }
        [BindProperty]
        public string label { get; set; }
        public Authorization()
        {
            Code = "";
            State = "";
            access_token = "";
            refresh_token = "";
        }
    }
}

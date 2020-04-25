namespace WebAppClient.Models
{
    public class AccessTokenRequest
    {
        public string Grant_Type { get; set; }
        public string Client_Id { get; set; }
        public string Redirect_Uri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }

    }
}

namespace Application.Common.Models
{
    public class AuthenticationDTO
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}

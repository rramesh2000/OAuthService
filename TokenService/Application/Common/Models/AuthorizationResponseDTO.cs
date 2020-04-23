namespace Application.Common.Models
{
    public class AuthorizationResponseDTO
    {
        public string Redirect_Uri { get; set; }
        public string State { get; set; }
        public string Code { get; set; }

    }

}

namespace Application.Common.Models
{
    public class RevocationDTO
    {
        public string token { get; set; }
        public UserDTO user { get; set; }
        public RefreshTokenDTO refresh { get; set; }
    }
}

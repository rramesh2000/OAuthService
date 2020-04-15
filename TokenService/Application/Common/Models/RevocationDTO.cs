namespace Application.Common.Models
{
    public class RevocationDTO
    {
        public UserDTO user { get; set; }
        public RefreshTokenDTO refresh { get; set; }
    }
}

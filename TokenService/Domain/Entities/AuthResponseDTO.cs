namespace Domain.Entities
{
    public class AuthResponseDTO
    {
        public string redirect_uri { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
    }
}

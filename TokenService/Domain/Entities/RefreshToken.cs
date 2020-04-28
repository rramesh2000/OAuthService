namespace Domain.Entities
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }

        public RefreshToken(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}

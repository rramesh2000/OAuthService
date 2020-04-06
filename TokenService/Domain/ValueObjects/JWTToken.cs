namespace Domain.ValueObjects
{
    public class JWTToken
    {
        public JWTToken()
        {
        }

        public JWTToken(Header header, Payload payload, Signature signature)
        {
            _header = header;
            _payload = payload;
            _signature = signature;
        }

        public Header _header { get; set; }
        public Payload _payload { get; set; }
        public Signature _signature { get; set; }

    }
}

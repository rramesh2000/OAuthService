namespace Domain
{
    public class Signature
    {
        public Signature()
        {
        }

        public Signature(string value)
        {
            _value = value;
        }

        public string _value { get; set; }
    }
}
namespace Domain
{
    public class Header
    {
        public Header()
        {
        }

        public Header(string alg, string typ)
        {
            this.alg = alg;
            this.typ = typ;
        }

        public string alg { get; set; }
        public string typ { get; set; }        

    }
}
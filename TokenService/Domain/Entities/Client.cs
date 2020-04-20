using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public Guid Client_Id { get; set; }
        public string Client_Secret { get; set; }
        public string ClientName { get; set; }
    }
}

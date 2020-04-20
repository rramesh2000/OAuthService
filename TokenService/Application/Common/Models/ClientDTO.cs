using System;
using System.Runtime.Serialization;

namespace Application.Common.Models
{
    public class ClientDTO
    {
        [IgnoreDataMember]
        public int ID { get; set; }

        [IgnoreDataMember]
        public Guid Client_Id { get; set; }

        [IgnoreDataMember]
        public string Client_Secret { get; set; }
        public string ClientName { get; set; }

    }
}

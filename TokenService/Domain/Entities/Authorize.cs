using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Authorize
    {
        public int ID { get; set; }
        public Guid UserId { get; set; } 
        public Guid Client_Id { get; set; } 
        public string Scope { get; set; } 
        public string Code { get; set; }

    }
}

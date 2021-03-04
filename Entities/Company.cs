using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class Company
    {
        public int ID { get; set; }

        public string CommercialName { get; set; }

        public UF State { get; set; }

        public string CPNJ { get; set; }
        
        public bool Active { get; set; }

        public virtual ICollection<Supplier> Supplier { get; set; }

    }
}

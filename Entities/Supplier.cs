using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Supplier
    {
        public int ID { get; set; }

        public string PersonResponsible { get; set; }

        public string CNPJ_CPF { get; set; }

        public string RG { get; set; }

        public DateTime BirthDate { get; set; }
        
        public DateTime DateRegistration { get; set; }
        
        public string Telephone { get; set; }
        
        public string Telephone2 { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

    }
}

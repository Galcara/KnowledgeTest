using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Models.CompanyModels
{
    public class CompanyUpdateViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nome Fantasia")]
        public string CommercialName { get; set; }

        [DisplayName("Unidade Federativa")]
        public UF State { get; set; }

        public string CPNJ { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }

        [DisplayName("Fornecedores")]
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}

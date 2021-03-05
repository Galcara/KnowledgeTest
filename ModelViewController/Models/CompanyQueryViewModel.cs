using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ModelViewController.Models
{
    public class CompanyQueryViewModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Nome Fantasia")]
        public string CommercialName { get; set; }

        [DisplayName("Unidade Federativa")]
        public UF State { get; set; }

        public string CPNJ { get; set; }

    }
}

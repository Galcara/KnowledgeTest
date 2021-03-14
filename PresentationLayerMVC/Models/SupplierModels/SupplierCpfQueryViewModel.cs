using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Models.SupplierModels
{
    public class SupplierCpfQueryViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nome Responsavel")]
        public string PersonResponsible { get; set; }

        [DisplayName("CPF")]
        public string CNPJ_CPF { get; set; }

        [DisplayName("Telefone")]
        public string Telephone { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }

        [DisplayName("Empresas")]
        public virtual ICollection<Company> Companies { get; set; }
    }
}

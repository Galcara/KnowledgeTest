using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Models.SupplierModels
{
    public class SupplierUpdateViewModel
    {
        [DisplayName("Nome Responsavel")]
        public string PersonResponsible { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string CNPJ_CPF { get; set; }

        public string RG { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Data de cadastro")]
        public DateTime DateRegistration { get; set; }

        [DisplayName("Telefone")]
        public string Telephone { get; set; }

        [DisplayName("Telefone 2")]
        public string Telephone2 { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }
    }
}

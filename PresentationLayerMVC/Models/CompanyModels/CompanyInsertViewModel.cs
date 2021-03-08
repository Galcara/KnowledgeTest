using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Models.CompanyModels
{
    public class CompanyInsertViewModel
    {
        [DisplayName("Nome Fantasia")]
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(50, ErrorMessage = "O nome deve estar entre 3 e 50 caracteres.", MinimumLength = 3)]
        public string CommercialName { get; set; }

        [DisplayName("Unidade Federativa")]
        [Required(ErrorMessage = "O Escolha um Estado")]
        public UF State { get; set; }

        public string CPNJ { get; set; }

    }
}

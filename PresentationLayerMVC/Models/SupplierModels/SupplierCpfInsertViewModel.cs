using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Models.SupplierModels
{      
    public class SupplierCpfInsertViewModel
    {
        [DisplayName("Nome do responsavel")]
        [Required(ErrorMessage = "Nome deve ser informado.")]
        public string PersonResponsible { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "O CPF deve ser informado")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CNPJ_CPF { get; set; }

        [Required(ErrorMessage = "O RG deve ser informado")]
        [StringLength(14, ErrorMessage = "O RG deve conter entre 7 e 14 caracteres", MinimumLength = 7)]
        public string RG { get; set; }

        [DisplayName("Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento deve ser informada.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O telefone deve ser informado.")]
        [StringLength(12, ErrorMessage = "O telefone deve conter 12 caracteres.")]
        public string Telephone { get; set; }

        [DisplayName("Telefone")]
        public string Telephone2 { get; set; }

        [DisplayName("Empresas")]
        [Required(ErrorMessage = "Selecione a Empresa fornecida")]
        public List<int> Companies { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        [StringLength(30, ErrorMessage = "Insira um nome de 1 a 50 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        public string Telefone { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }
    }
}
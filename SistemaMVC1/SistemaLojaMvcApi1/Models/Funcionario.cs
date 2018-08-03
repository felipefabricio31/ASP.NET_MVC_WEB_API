using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        [StringLength(30, ErrorMessage = "Insira um nome de 1 a 30 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        public string Sobrenome { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Salário")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Salario { get; set; }

        [Display(Name = "Comissão")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float Comissao { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Você precisa inserir a {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Você precisa inserir a {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public int Idade { get { return DateTime.Now.Year - DataNascimento.Year; } }

        //Relacionamento de Tabela
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        public int TipoDocumentoId { get; set; }    
        
        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}
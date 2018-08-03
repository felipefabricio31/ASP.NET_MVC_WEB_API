using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLojaMvcApi1.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        [StringLength(30, ErrorMessage = "Insira um nome de 1 a 50 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Preco { get; set; }

        [Display(Name = "Data da Compra")]
        [Required(ErrorMessage = "Você precisa inserir a {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UltimaCompra { get; set; }

        [Display(Name = "Estoque")]
        [Required(ErrorMessage = "Você precisa inserir o {0}")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Estoque { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }
        
        //Aula38
        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }

        //ICollection, porque vamos receber informacao
        public virtual ICollection<OrdemDetalhe> OrdensDetalhes { get; set; }
    }
}
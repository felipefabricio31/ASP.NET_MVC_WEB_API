using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class FornecedorProduto
    {
        [Key]
        public int FornecedorProdutoId { get; set; }

        public int FornecedorId { get; set; }

        //Deve ser igual ao ID da classe do Produto
        public int ProdutoId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
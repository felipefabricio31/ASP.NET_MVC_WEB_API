using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class FornecedorProduto
    {
        [Key]
        public int FornecedorProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }

        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }

        [JsonIgnore]
        public virtual Produto Produto { get; set; }
    }
}
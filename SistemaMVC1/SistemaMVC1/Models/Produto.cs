using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaMVC1.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        public string Descricao { get; set; }

        public decimal Preco  { get; set; }

        public DateTime UltimaCompra { get; set; }

        public float Estoque { get; set; }

        //Acresentar novo campo aula 16
        public string Comentario { get; set; }
    }
}
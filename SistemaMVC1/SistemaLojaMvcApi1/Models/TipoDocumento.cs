using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class TipoDocumento
    {
        [Key]
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        //Relacionamento de Tabelas
        public virtual ICollection<Funcionario> Funcionaios { get; set; }

        public virtual ICollection<Customizar> Customizacao { get; set; }
    }
}
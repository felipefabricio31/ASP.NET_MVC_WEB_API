using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class Ordem
    {
        [Key]
        public int OrdemId { get; set; }

        [Display(Name = "Data da Ordem")]
        [Required(ErrorMessage = "Você precisa inserir a {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrdemData { get; set; }

        public int CustomizarId { get; set; }

        public OrdemStatus OrdemStatus { get; set; }

        public virtual Customizar Customizar { get; set; }

        //ICollection, porque vamos receber informacao
        public virtual ICollection<OrdemDetalhe> OrdensDetalhes { get; set; }
    }
}
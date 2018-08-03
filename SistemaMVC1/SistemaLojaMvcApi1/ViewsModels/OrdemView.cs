using SistemaLojaMvcApi1.Models;
using System.Collections.Generic;

namespace SistemaLojaMvcApi1.ViewsModels
{
    public class OrdemView
    {
        //Mostra a ordem dos produtos

        public Customizar Customizar { get; set; }

        public ProdutoOrdem Produto { get; set; }

        public List<ProdutoOrdem> Produtos { get; set; }

    }
}
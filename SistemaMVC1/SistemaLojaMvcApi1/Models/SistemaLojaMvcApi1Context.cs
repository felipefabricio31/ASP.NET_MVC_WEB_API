using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SistemaLojaMvcApi1.Models
{
    public class SistemaLojaMvcApi1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SistemaLojaMvcApi1Context() : base("name=SistemaLojaMvcApi1Context")
        {
        }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.TipoDocumento> TipoDocumentoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.Funcionario> Funcionarios { get; set; }

        //Configurar para não excluir em Cascata
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.Fornecedor> Fornecedors { get; set; }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.Customizar> Customizars { get; set; }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.Ordem> Ordem { get; set; }

        public System.Data.Entity.DbSet<SistemaLojaMvcApi1.Models.OrdemDetalhe> OrdemDetalhe { get; set; }
    }
}

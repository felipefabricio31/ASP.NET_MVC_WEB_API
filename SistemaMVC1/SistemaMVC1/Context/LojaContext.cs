using SistemaMVC1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaMVC1.Context
{
    //Clase BD
    public class LojaContext : DbContext
    {
        //Referência ao Produto
        public DbSet<Produto>Produto { get; set; }
    }
}
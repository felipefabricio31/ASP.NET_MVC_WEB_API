namespace SistemaLojaMvcApi1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SistemaLojaMvcApi1.Models.SistemaLojaMvcApi1Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SistemaLojaMvcApi1.Models.SistemaLojaMvcApi1Context";
            //Sempre que utilizar migração automatica está linha deverá ser true.
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SistemaLojaMvcApi1.Models.SistemaLojaMvcApi1Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

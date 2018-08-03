namespace SistemaMVC1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComentarioFieldModelProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtoes", "Comentario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtoes", "Comentario");
        }
    }
}

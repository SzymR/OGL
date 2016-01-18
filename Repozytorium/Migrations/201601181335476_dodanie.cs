namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtrybutWartosc", "IdAtrybut", "dbo.Atrybut");
            DropForeignKey("dbo.Kategoria_Atrybut", "IdAtrybut", "dbo.Atrybut");
            DropPrimaryKey("dbo.Atrybut");
            AlterColumn("dbo.Atrybut", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Atrybut", "Id");
            AddForeignKey("dbo.AtrybutWartosc", "IdAtrybut", "dbo.Atrybut", "Id");
            AddForeignKey("dbo.Kategoria_Atrybut", "IdAtrybut", "dbo.Atrybut", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kategoria_Atrybut", "IdAtrybut", "dbo.Atrybut");
            DropForeignKey("dbo.AtrybutWartosc", "IdAtrybut", "dbo.Atrybut");
            DropPrimaryKey("dbo.Atrybut");
            AlterColumn("dbo.Atrybut", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Atrybut", "Id");
            AddForeignKey("dbo.Kategoria_Atrybut", "IdAtrybut", "dbo.Atrybut", "Id");
            AddForeignKey("dbo.AtrybutWartosc", "IdAtrybut", "dbo.Atrybut", "Id");
        }
    }
}

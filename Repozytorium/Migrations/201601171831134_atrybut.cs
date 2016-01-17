namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atrybut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atrybut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AtrybutWartosc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAtrybut = c.Int(nullable: false),
                        Wartosc = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atrybut", t => t.IdAtrybut)
                .Index(t => t.IdAtrybut);
            
            CreateTable(
                "dbo.Kategoria_Atrybut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAtrybut = c.Int(nullable: false),
                        IdKategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atrybut", t => t.IdAtrybut)
                .ForeignKey("dbo.Kategoria", t => t.IdKategoria)
                .Index(t => t.IdAtrybut)
                .Index(t => t.IdKategoria);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kategoria_Atrybut", "IdKategoria", "dbo.Kategoria");
            DropForeignKey("dbo.Kategoria_Atrybut", "IdAtrybut", "dbo.Atrybut");
            DropForeignKey("dbo.AtrybutWartosc", "IdAtrybut", "dbo.Atrybut");
            DropIndex("dbo.Kategoria_Atrybut", new[] { "IdKategoria" });
            DropIndex("dbo.Kategoria_Atrybut", new[] { "IdAtrybut" });
            DropIndex("dbo.AtrybutWartosc", new[] { "IdAtrybut" });
            DropTable("dbo.Kategoria_Atrybut");
            DropTable("dbo.AtrybutWartosc");
            DropTable("dbo.Atrybut");
        }
    }
}

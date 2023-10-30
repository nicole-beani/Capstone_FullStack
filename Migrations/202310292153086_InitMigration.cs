namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetPrenotazione",
                c => new
                    {
                        IdDetPrenotazione = c.Int(nullable: false, identity: true),
                        Quantita = c.Int(),
                        IdGrotte = c.Int(),
                        IdPrenotazione = c.Int(),
                    })
                .PrimaryKey(t => t.IdDetPrenotazione)
                .ForeignKey("dbo.Grotte", t => t.IdGrotte)
                .ForeignKey("dbo.Prenotazione", t => t.IdPrenotazione)
                .Index(t => t.IdGrotte)
                .Index(t => t.IdPrenotazione);
            
            CreateTable(
                "dbo.Grotte",
                c => new
                    {
                        IdGrotte = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descrizione = c.String(),
                        Foto = c.String(),
                        Prezzo = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.IdGrotte);
            
            CreateTable(
                "dbo.Prenotazione",
                c => new
                    {
                        IdPrenotazione = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(storeType: "date"),
                        Importo = c.Decimal(storeType: "money"),
                        IdOrari = c.Int(),
                        IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdPrenotazione)
                .ForeignKey("dbo.Orari", t => t.IdOrari)
                .ForeignKey("dbo.User", t => t.IdUser)
                .Index(t => t.IdOrari)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Orari",
                c => new
                    {
                        IdOrari = c.Int(nullable: false, identity: true),
                        OrariGrotte = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdOrari);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                        Cognome = c.String(maxLength: 50),
                        Username = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Ruolo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prenotazione", "IdUser", "dbo.User");
            DropForeignKey("dbo.Prenotazione", "IdOrari", "dbo.Orari");
            DropForeignKey("dbo.DetPrenotazione", "IdPrenotazione", "dbo.Prenotazione");
            DropForeignKey("dbo.DetPrenotazione", "IdGrotte", "dbo.Grotte");
            DropIndex("dbo.Prenotazione", new[] { "IdUser" });
            DropIndex("dbo.Prenotazione", new[] { "IdOrari" });
            DropIndex("dbo.DetPrenotazione", new[] { "IdPrenotazione" });
            DropIndex("dbo.DetPrenotazione", new[] { "IdGrotte" });
            DropTable("dbo.User");
            DropTable("dbo.Orari");
            DropTable("dbo.Prenotazione");
            DropTable("dbo.Grotte");
            DropTable("dbo.DetPrenotazione");
        }
    }
}

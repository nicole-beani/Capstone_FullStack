namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletetableDetPrenotazione : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetPrenotazione", "IdGrotte", "dbo.Grotte");
            DropForeignKey("dbo.DetPrenotazione", "IdPrenotazione", "dbo.Prenotazione");
            DropIndex("dbo.DetPrenotazione", new[] { "IdGrotte" });
            DropIndex("dbo.DetPrenotazione", new[] { "IdPrenotazione" });
            DropTable("dbo.DetPrenotazione");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.IdDetPrenotazione);
            
            CreateIndex("dbo.DetPrenotazione", "IdPrenotazione");
            CreateIndex("dbo.DetPrenotazione", "IdGrotte");
            AddForeignKey("dbo.DetPrenotazione", "IdPrenotazione", "dbo.Prenotazione", "IdPrenotazione");
            AddForeignKey("dbo.DetPrenotazione", "IdGrotte", "dbo.Grotte", "IdGrotte");
        }
    }
}

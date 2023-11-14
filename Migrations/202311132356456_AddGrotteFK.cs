namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrotteFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prenotazione", "IdGrotte", c => c.Int());
            CreateIndex("dbo.Prenotazione", "IdGrotte");
            AddForeignKey("dbo.Prenotazione", "IdGrotte", "dbo.Grotte", "IdGrotte");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prenotazione", "IdGrotte", "dbo.Grotte");
            DropIndex("dbo.Prenotazione", new[] { "IdGrotte" });
            DropColumn("dbo.Prenotazione", "IdGrotte");
        }
    }
}

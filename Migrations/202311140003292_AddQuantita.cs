namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantita : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prenotazione", "Quantita", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prenotazione", "Quantita");
        }
    }
}

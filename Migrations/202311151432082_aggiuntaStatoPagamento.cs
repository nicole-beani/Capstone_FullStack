namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aggiuntaStatoPagamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prenotazione", "StatoPagamento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prenotazione", "StatoPagamento");
        }
    }
}

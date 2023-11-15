namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GrotteId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orari", "GrotteId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orari", "GrotteId");
        }
    }
}

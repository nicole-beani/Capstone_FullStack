namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteGrotteId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orari", "GrotteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orari", "GrotteId", c => c.Int(nullable: false));
        }
    }
}

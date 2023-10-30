namespace WaveTheCave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Generale : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grotte", "Prezzo", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grotte", "Prezzo", c => c.Decimal(storeType: "money"));
        }
    }
}

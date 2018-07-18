namespace CrystalBallpro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoriesModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Units", c => c.String());
            AddColumn("dbo.Inventories", "Expiration", c => c.String());
            AddColumn("dbo.Inventories", "LastOrdered", c => c.String());
            DropColumn("dbo.Inventories", "experation");
            DropColumn("dbo.Inventories", "LastOrderd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "LastOrderd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "experation", c => c.DateTime(nullable: false));
            DropColumn("dbo.Inventories", "LastOrdered");
            DropColumn("dbo.Inventories", "Expiration");
            DropColumn("dbo.Inventories", "Units");
        }
    }
}

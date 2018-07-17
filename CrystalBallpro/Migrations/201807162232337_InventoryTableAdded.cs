namespace CrystalBallpro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stock = c.Int(nullable: false),
                        experation = c.DateTime(nullable: false),
                        LastOrderd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inventories");
        }
    }
}

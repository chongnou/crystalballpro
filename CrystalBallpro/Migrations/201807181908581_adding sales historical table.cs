namespace CrystalBallpro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingsaleshistoricaltable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesHistoricals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfEvents = c.Int(nullable: false),
                        NumberOfEmployees = c.Int(nullable: false),
                        SalesIncreasePercent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalesHistoricals");
        }
    }
}

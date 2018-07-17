namespace CrystalBallpro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeIDnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Availabilities", "AdminID", "dbo.Admins");
            DropForeignKey("dbo.Availabilities", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Availabilities", new[] { "AdminID" });
            DropIndex("dbo.Availabilities", new[] { "EmployeeID" });
            AlterColumn("dbo.Availabilities", "AdminID", c => c.Int());
            AlterColumn("dbo.Availabilities", "EmployeeID", c => c.Int());
            CreateIndex("dbo.Availabilities", "AdminID");
            CreateIndex("dbo.Availabilities", "EmployeeID");
            AddForeignKey("dbo.Availabilities", "AdminID", "dbo.Admins", "ID");
            AddForeignKey("dbo.Availabilities", "EmployeeID", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Availabilities", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Availabilities", "AdminID", "dbo.Admins");
            DropIndex("dbo.Availabilities", new[] { "EmployeeID" });
            DropIndex("dbo.Availabilities", new[] { "AdminID" });
            AlterColumn("dbo.Availabilities", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Availabilities", "AdminID", c => c.Int(nullable: false));
            CreateIndex("dbo.Availabilities", "EmployeeID");
            CreateIndex("dbo.Availabilities", "AdminID");
            AddForeignKey("dbo.Availabilities", "EmployeeID", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Availabilities", "AdminID", "dbo.Admins", "ID", cascadeDelete: true);
        }
    }
}

namespace CrystalBallpro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdminID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        DayID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Admins", t => t.AdminID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.DayID, cascadeDelete: true)
                .Index(t => t.AdminID)
                .Index(t => t.EmployeeID)
                .Index(t => t.DayID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.Weeks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Availabilities", "DayID", "dbo.Weeks");
            DropForeignKey("dbo.Availabilities", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Availabilities", "AdminID", "dbo.Admins");
            DropForeignKey("dbo.Admins", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "ApplicationUserID" });
            DropIndex("dbo.Availabilities", new[] { "DayID" });
            DropIndex("dbo.Availabilities", new[] { "EmployeeID" });
            DropIndex("dbo.Availabilities", new[] { "AdminID" });
            DropIndex("dbo.Admins", new[] { "ApplicationUserID" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Weeks");
            DropTable("dbo.Employees");
            DropTable("dbo.Availabilities");
            DropTable("dbo.Admins");
        }
    }
}

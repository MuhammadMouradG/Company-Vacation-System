namespace DestinationViews.Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        EmpVacationTypeID = c.Int(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Accepted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.EmpVacationTypes", t => t.EmpVacationTypeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.EmpVacationTypeID);
            
            CreateTable(
                "dbo.EmpVacationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        DefaultBalance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Full_Name = c.String(nullable: false),
                        Email = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmpVacationBalances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        EmpVacationTypeID = c.Int(nullable: false),
                        NewBalance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.EmpVacationTypes", t => t.EmpVacationTypeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.EmpVacationTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpVacationBalances", "EmpVacationTypeID", "dbo.EmpVacationTypes");
            DropForeignKey("dbo.EmpVacationBalances", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "UserID", "dbo.Users");
            DropForeignKey("dbo.Requests", "EmpVacationTypeID", "dbo.EmpVacationTypes");
            DropForeignKey("dbo.Requests", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmpVacationBalances", new[] { "EmpVacationTypeID" });
            DropIndex("dbo.EmpVacationBalances", new[] { "EmployeeID" });
            DropIndex("dbo.Requests", new[] { "EmpVacationTypeID" });
            DropIndex("dbo.Requests", new[] { "EmployeeID" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropTable("dbo.EmpVacationBalances");
            DropTable("dbo.Users");
            DropTable("dbo.EmpVacationTypes");
            DropTable("dbo.Requests");
            DropTable("dbo.Employees");
        }
    }
}

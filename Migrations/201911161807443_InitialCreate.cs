namespace Kompanija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        departmentNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeNo = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        salary = c.Int(nullable: false),
                        lastModifyDate = c.DateTime(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "departmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "departmentId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}

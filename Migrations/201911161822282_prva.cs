namespace Kompanija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "departmentName", c => c.String(maxLength: 20));
            AddColumn("dbo.Departments", "departmentLocation", c => c.String(maxLength: 20));
            AlterColumn("dbo.Employees", "EmployeeName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmployeeName", c => c.String());
            DropColumn("dbo.Departments", "departmentLocation");
            DropColumn("dbo.Departments", "departmentName");
        }
    }
}

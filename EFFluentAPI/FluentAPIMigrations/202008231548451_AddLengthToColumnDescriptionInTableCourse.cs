namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLengthToColumnDescriptionInTableCourse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String());
        }
    }
}

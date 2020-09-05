namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteFalse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AuthorID", "dbo.Authors");
            AddForeignKey("dbo.Courses", "AuthorID", "dbo.Authors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AuthorID", "dbo.Authors");
            AddForeignKey("dbo.Courses", "AuthorID", "dbo.Authors", "ID", cascadeDelete: true);
        }
    }
}

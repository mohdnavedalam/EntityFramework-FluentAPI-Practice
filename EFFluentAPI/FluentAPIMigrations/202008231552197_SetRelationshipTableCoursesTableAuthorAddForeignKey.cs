namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRelationshipTableCoursesTableAuthorAddForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Author_ID", "dbo.Authors");
            DropIndex("dbo.Courses", new[] { "Author_ID" });
            RenameColumn(table: "dbo.Courses", name: "Author_ID", newName: "AuthorID");
            AlterColumn("dbo.Courses", "AuthorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "AuthorID");
            AddForeignKey("dbo.Courses", "AuthorID", "dbo.Authors", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AuthorID", "dbo.Authors");
            DropIndex("dbo.Courses", new[] { "AuthorID" });
            AlterColumn("dbo.Courses", "AuthorID", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "AuthorID", newName: "Author_ID");
            CreateIndex("dbo.Courses", "Author_ID");
            AddForeignKey("dbo.Courses", "Author_ID", "dbo.Authors", "ID");
        }
    }
}

namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRelationShipTableCoursesTableTagsChangeColumnName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TagCourses", newName: "CourseTags");
            DropPrimaryKey("dbo.CourseTags");
            AddPrimaryKey("dbo.CourseTags", new[] { "Course_ID", "Tag_ID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CourseTags");
            AddPrimaryKey("dbo.CourseTags", new[] { "Tag_ID", "Course_ID" });
            RenameTable(name: "dbo.CourseTags", newName: "TagCourses");
        }
    }
}

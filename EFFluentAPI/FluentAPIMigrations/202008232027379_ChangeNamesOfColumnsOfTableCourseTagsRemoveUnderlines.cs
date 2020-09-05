namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNamesOfColumnsOfTableCourseTagsRemoveUnderlines : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseTags", name: "Course_ID", newName: "CourseID");
            RenameColumn(table: "dbo.CourseTags", name: "Tag_ID", newName: "TagID");
            RenameIndex(table: "dbo.CourseTags", name: "IX_Course_ID", newName: "IX_CourseID");
            RenameIndex(table: "dbo.CourseTags", name: "IX_Tag_ID", newName: "IX_TagID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourseTags", name: "IX_TagID", newName: "IX_Tag_ID");
            RenameIndex(table: "dbo.CourseTags", name: "IX_CourseID", newName: "IX_Course_ID");
            RenameColumn(table: "dbo.CourseTags", name: "TagID", newName: "Tag_ID");
            RenameColumn(table: "dbo.CourseTags", name: "CourseID", newName: "Course_ID");
        }
    }
}

namespace EFFluentAPI.FluentAPIMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRelationShipTableCoursesTableCoverOneToOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Covers", "ID", "dbo.Courses");
            DropIndex("dbo.Covers", new[] { "ID" });
            DropTable("dbo.Covers");
        }
    }
}

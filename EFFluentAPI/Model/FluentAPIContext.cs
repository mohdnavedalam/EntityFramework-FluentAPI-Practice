using System.Data.Entity;
using EFFluentAPI.EntityConfigurations;


namespace EFFluentAPI.Model
{
    public class FluentAPIContext : DbContext
    {
        public FluentAPIContext() : base("name=FluentAPIContext") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Author> Authors { get; set; }
        //public DbSet<Cover> Covers { get; set; }    //Add or not, the program will work

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());

            //modelBuilder.Entity<Course>()
            //    .Property(c => c.Name)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //modelBuilder.Entity<Course>()
            //    .Property(c => c.Description)
            //    .IsRequired()
            //    .HasMaxLength(2000);

            //modelBuilder.Entity<Course>()
            //    .HasRequired(c => c.Author)
            //    .WithMany(a => a.Courses)
            //    .HasForeignKey(c => c.AuthorID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Tags)
            //    .WithMany(t => t.Courses)
            //    //.Map(m => m.ToTable("CourseTags"));
            //    .Map(m =>
            //    {
            //        m.ToTable("CourseTags");
            //        m.MapLeftKey("CourseID");
            //        m.MapRightKey("TagID");
            //    });

            //modelBuilder.Entity<Course>()
            //    .HasRequired(c => c.Cover)
            //    .WithRequiredPrincipal(c => c.Course);

            base.OnModelCreating(modelBuilder);
        }
    }
}

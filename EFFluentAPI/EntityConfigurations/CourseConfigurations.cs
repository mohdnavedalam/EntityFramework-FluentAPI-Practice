using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

using EFFluentAPI.Model;

namespace EFFluentAPI.EntityConfigurations
{
    class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);


            Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(2000);


            HasRequired(c => c.Author)
                    .WithMany(a => a.Courses)
                    .HasForeignKey(c => c.AuthorID)
                    .WillCascadeOnDelete(false);


            HasMany(c => c.Tags)
                    .WithMany(t => t.Courses)
                    //.Map(m => m.ToTable("CourseTags"));
                    //.Map(m => m.ToTable("CourseTags").MapLeftKey("CourseID").MapRightKey("TagID"));
                    .Map(m =>
                    {
                        m.ToTable("CourseTags");
                        m.MapLeftKey("CourseID");
                        m.MapRightKey("TagID");
                    });

            HasRequired(c => c.Cover)
            .WithRequiredPrincipal(c => c.Course);
        }
    }
}

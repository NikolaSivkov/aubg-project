using TNTWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNTWebApp.Data.EntityConfig
{
    public class UserCourseMapping : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.StudentId });
            builder.HasOne(x => x.Student)
                .WithMany(x => x.Courses)
                .HasForeignKey(x=>x.StudentId);

            builder.HasOne(x => x.Course)
              .WithMany(x => x.Students)
              .HasForeignKey(x => x.CourseId);

        }
    }
}

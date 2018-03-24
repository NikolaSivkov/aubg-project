using HackAu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackAu.Data.EntityConfig
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.Courses)
                .HasForeignKey(x=>x.TeacherId);
            
           
        }
    }
}

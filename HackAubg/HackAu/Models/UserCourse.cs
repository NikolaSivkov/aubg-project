using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNTWebApp.Models
{
    public class UserCourse
    {
        public int CourseId { get; set; }
        public Course  Course { get; set; }
        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }
    }
}

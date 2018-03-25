using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNTWebApp.Models
{
    public class Course
    {
        public Course()
        {
            Students = new List<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Subject { get; set; }


        public  string TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}

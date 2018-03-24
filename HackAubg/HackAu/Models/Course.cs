using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackAu.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }
        public ICollection<ApplicationUser> Students => new List<ApplicationUser>();
    }
}

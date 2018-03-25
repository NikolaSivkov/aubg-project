﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNTWebApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }
        public ICollection<UserCourse> Students => new List<UserCourse>();
    }
}

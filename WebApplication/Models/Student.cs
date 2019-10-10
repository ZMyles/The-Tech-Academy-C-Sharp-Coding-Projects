using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.ContosoUniversity.Models;

namespace WebApplication.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
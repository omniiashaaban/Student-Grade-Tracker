using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
   public class StudentCourse
    {
        public Course Course { get; set; }
        public double Grade     { get; set; }
        public StudentCourse(Course course, double grade)
        {
            Course = course;
            Grade = grade;
        }
        public override string ToString()
        {
            return $"{Course.Name} , Grade: {Grade}";
        }
    }
}

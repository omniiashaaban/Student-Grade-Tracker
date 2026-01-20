using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
    public class Student
    {
        
        public string Name { get; set; } // اسم الطالب 

        public List<StudentCourse> Courses;     // اسماء الكورسات 
        public int Attendance { get; set; } 

        public Student(string name)
        {
            Name = name;
            Courses = new List<StudentCourse>();  
            Attendance = 0;                       
        }

        #region Assign grades 
        public static void AssignGrade(Student student, Course course, double score)
        {

            var existing = student.Courses.FirstOrDefault(sc => sc.Course == course);     //عشان ميضفش درجتين لنفس الماده
            if (existing != null)
            {
                existing.Grade = score;
            }
            else
            {
                student.Courses.Add(new StudentCourse(course, score));
            }
        }
        #endregion


        public override string ToString()
        {
            string coursesInfo;

            if (Courses.Count == 0) coursesInfo = "No courses enrolled";
            else coursesInfo = string.Join(", ", Courses);

            return $"Name: {Name}, Attendance: {Attendance}%, Courses: [{coursesInfo}]";
        }
    }
}

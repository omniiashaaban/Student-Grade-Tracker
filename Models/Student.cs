using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OOP_Pro.Models
{
    public class Student
    {
        private static int _counter = 1; // عداد للـ ID

        public int Id { get; private set; }
        public string Name { get; set; } // اسم الطالب 

        public List<StudentCourse> Courses;     // اسماء الكورسات 
        public int Attendance { get; set; } 

        public Student(string name)
        {
            Id = _counter++;        // توليد ID تلقائي

            Name = name;
            Courses = new List<StudentCourse>();  
            Attendance = 0;                       
        }



        #region Assign grades 
        public static void AssignGrade(List<Student> students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"\nStudent: {student.Name}");

                if (student.Courses == null || student.Courses.Count == 0)
                {
                    Console.WriteLine("  No courses found for this student.");       // عشان لو طالب مش عنده كورسات يسكبه 
                }

                foreach (StudentCourse sc in student.Courses)
                {
                    Console.Write($"Enter grade for {sc.Course.Name}: ");

                    double grade;
                    while (!double.TryParse(Console.ReadLine(), out grade))
                    {
                        Console.Write("Invalid grade, enter again: ");
                    }

                    sc.Grade = grade;
                }
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

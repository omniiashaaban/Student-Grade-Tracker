using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OOP_Pro.Models
{
    public class Student
    {
        private static int _counter = 1; // عداد للـ ID

        public int Id { get; private set; }
        public string Name { get; set; } // اسم الطالب 

        public List<StudentCourse> Courses;     // اسماء الكورسات 
        public Student(string name)
        {
            Id = _counter++;        // توليد ID تلقائي

            Name = name;
            Courses = new List<StudentCourse>();  
        }



        #region Assign grades 

        public static void AssignGrade(List<Student> students, List<Course> courses, string gradesPath)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"\nStudent: {student.Name}\n");

                FileManager.LoadStudentGradesFromText(gradesPath, student);

                foreach (var co in courses)
                {
                    Console.Write($"Enter grade for {co.Name}: ");

                    double grade;
                    while (!double.TryParse(Console.ReadLine(), out grade))
                        Console.Write("Invalid grade, enter again: ");

                    var existing = student.Courses.FirstOrDefault(sc =>
                        sc.Course.Name.Equals(co.Name, StringComparison.OrdinalIgnoreCase));

                    if (existing != null)
                    {
                        existing.Grade = grade;
                    }
                    else
                    {
                        student.Courses.Add(new StudentCourse(co, grade));
                    }
                }

                FileManager.SaveStudentGradesToText(gradesPath, student);
                GradeManager.CalculateGPA(student);
            }
        }

        #endregion

      

        public override string ToString()
        {
            string coursesInfo;

            if (Courses.Count == 0) coursesInfo = "No courses enrolled";
            else coursesInfo = string.Join(", ", Courses);

            return $"Name: {Name}, Courses: [{coursesInfo}]";
        }
    }
}

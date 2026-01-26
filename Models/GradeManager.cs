using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
   public static class GradeManager
    {



     
            public static double CalculateGPA(Student student)
            {
                if (student.Courses == null || student.Courses.Count == 0)
                {
                    Console.WriteLine($"{student.Name} has no registered courses.");
                    return 0;
                }

                double totalPoints = 0;
                double totalHours = 0;

                foreach (var sc in student.Courses)
                {
                    totalPoints += sc.Grade * sc.Course.CreditHours;
                    totalHours += sc.Course.CreditHours;
                }

                if (totalHours == 0)
                    return 0;

                double gpa = Math.Round((totalPoints / totalHours) / 25, 2);

                Console.WriteLine($"The GPA of {student.Name} is {gpa}");
                return gpa;
            }

     
        #region Identify at-risk and top students
        public static void riskandtopstudents(List<Student>students)
        {

            foreach (var student in students)
            {
            FileManager.LoadStudentGradesFromText("Grades.txt", student);

                if (student.Courses.Count == 0)
                    continue;

                double gpa = CalculateGPA(student);

                if (gpa >= 3.6)
                    Console.WriteLine($"{student.Name} is a TOP student \n");
                else if (gpa <= 2.4)
                    Console.WriteLine($"{student.Name} is at RISK \n");
                else
                    Console.WriteLine($"{student.Name} is in the average range\n");
            }
        }
        #endregion


    }

}

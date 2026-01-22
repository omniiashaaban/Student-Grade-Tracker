using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
   public static class GradeManager
    {


        #region GPA calculation 
        public static double CalculateGPA(Student student)
        {
            double total = 0;
            double totalhour = 0;

            foreach (var sc in student.Courses)
            {
                total += sc.Grade * sc.Course.CreditHours;
                totalhour += sc.Course.CreditHours;
            }

            double gpa100 = Math.Round(total / totalhour);
            double gpa = gpa100 / 25;

            Console.WriteLine($"The GPA of {student.Name} is {gpa}");
            return gpa;
        }


        #endregion


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

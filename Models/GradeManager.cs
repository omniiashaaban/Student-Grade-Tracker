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

            int size = student.Courses.Count;
            double total = 0;
            double hour = 0;
            double totalhour = 0;
            for (int i = 0; i < size; i++)
            {
                double grade = student.Courses.ElementAt(i).Grade;
                hour = student.Courses.ElementAt(i).Course.CreditHours;
                totalhour += student.Courses.ElementAt(i).Course.CreditHours;


                total += grade * hour;

               
            }
            double gpa100 = Math.Round(total / totalhour);
            double gpa = gpa100 / 25;
            Console.WriteLine($"The GPA of {student.Name}  is {gpa} .");

            return gpa;
        } 

        #endregion
       

        #region Identify at-risk and top students
        public static void riskandtopstudents(Student[] allstudents)
        {

            foreach (var student in allstudents)
            {
                double gpa = CalculateGPA(student);
                if (gpa >= 3.6)
                    Console.WriteLine($"{student.Name} is a Top Student");
                else if (gpa <= 2.4)
                    Console.WriteLine($"{student.Name} is At-Risk");
            }
        } 
        #endregion


    }

}

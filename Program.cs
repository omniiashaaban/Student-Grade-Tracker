using OOP_Pro.Classes;
using OOP_Pro.Models;

namespace OOP_Pro
{
    internal class Program
    {
        
        
        /* Added by Fathi 
             
             بيدخل اللي اسمة TextReading 
            و بيرجع بالبيانات الموجوده في الملف 
            و بيخزنها في Arry 

             */
        static void Main(string[] args)
        {
            #region fathi
            //Student[] students = TextReading.ReadStudentsFromFile("students.txt");

            //Console.WriteLine($"All Students");



            //foreach (var s in students)
            //{
            //    Console.WriteLine($"{s.Name} - {s.Course} - Grade: {s.Grade}");
            //}


            //Console.WriteLine($"\n");


            //if (students != null)
            //{
            //    StudentOrgnize.StudentAnalyze(students);
            //}
            //else
            //{
            //    Console.WriteLine("No students found.");
            //} 
            #endregion


            #region Testing by omnia
            Course math = new Course("Math", 3);
            Course Physics = new Course("Physics", 4);


            Student s1 = new Student("Ahmed");
            Student s2 = new Student("Mostafa");

            Student.AssignGrade(s1, math, 100);
            Student.AssignGrade(s1, Physics, 90);
            GradeManager.CalculateGPA(s1); Console.WriteLine("========>Test for calcGPA");

            Console.WriteLine("-----------------------");

            Student.AssignGrade(s2, math, 20);
            Student.AssignGrade(s2, Physics, 50);
            GradeManager.CalculateGPA(s2); Console.WriteLine("========>Test for calcGPA");

            Console.WriteLine("-----------------------");

            Student[] students = new Student[2];
            students[0]=s1; students[1]=s2;

           


            GradeManager.riskandtopstudents(students); Console.WriteLine("========>Test for risk , top");

            Console.WriteLine("-----------------------");

            Console.WriteLine(s1);


            #endregion
        }
    }
}

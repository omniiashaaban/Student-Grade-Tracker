
using OOP_Pro.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;

namespace OOP_Pro
{
    internal class Program
    {
        /*  عملتها في 
        Function 
        علشان هنستخدمها اكتر من مرة 
         */

        /*Added by => Fathi          21-1-2026 : 10:34 */
        static string Questions()
        {

            Console.WriteLine("How Can i Help You ?");
            Console.WriteLine(" 1 -  Course ? ");
            Console.WriteLine(" 2 -  Student ?");
            Console.WriteLine(" 3 - Assign Grade?");
            Console.WriteLine(" 4 - risk and top students?");
            Console.WriteLine(" 5 - Exit");

            Console.WriteLine("\n");

            string USerChoise = Console.ReadLine();

            return USerChoise;
        }


        static void Main(string[] args)
        {
            List<Course> courses = new List<Course>();
            List<Student> student = new List<Student>();

            // قراءة المواد من الشيت
            var coursesData = FileManager.ReadCoursesFromExcel("Courses.xlsx");// ليست للكورسات 
            var StudentsData = FileManager.ReadStudentsFromExcel("Students.xlsx"); // ليست للطلاب



            string Return_User_Result = Questions();


            Console.WriteLine("\n");

            var defultVAlu = "y"; // قيمة افتراضية لااضافة كورس
            var inputstudent = "y"; // قيمة افتراضية لااضافة طالب 

            /*1*/
            #region Course

            if (Return_User_Result.ToLower() == "1")
            {
                Console.WriteLine("1 - Add Course ");
                Console.WriteLine("2 - All Courses ");
                Console.WriteLine("\n");

                string CourseChois = Console.ReadLine();

                // لو كتب 1 هيضيف كورس 
                if (CourseChois == "1")
                {
                    while (defultVAlu.ToLower() == "y")
                    {
                        Console.WriteLine("Enter subject name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter subject hours:");
                        int hours;

                        while (!int.TryParse(Console.ReadLine(), out hours))
                        {
                            Console.WriteLine("Enter subject hours again:");
                        }

                        courses.Add(new Course(name, hours));

                        Console.WriteLine("Do you want to add another subject? (y/n)");
                        defultVAlu = Console.ReadLine();
                    }


                    courses.AddRange(coursesData);// دمج المواد القديمة والجديدة

                    // حفظ كل المواد في Excel
                    FileManager.SaveCoursesToExcel(courses, "Courses.xlsx");

                    Console.WriteLine("Courses saved successfully.\n");
                }
                // لو كتب 2 هيعرض الكورسات  
                else
                {

                    Console.WriteLine("Courses from Excel:");
                    Console.WriteLine("\n");

                    var allCourses = FileManager.ReadCoursesFromExcel("Courses.xlsx");
                    foreach (var item in allCourses)
                    {
                        Console.WriteLine($"Course Name: {item.Name}  ||  Hours:{item.CreditHours}");
                    }
                }
            }

            #endregion
            /*2*/
            #region student
            /* 
             عملت هنا 
             else if  = > علشان هيكون عندي اكتر من Condition 
             */

            else if (Return_User_Result.ToLower() == "2")
            {
                var oldStudents = FileManager.ReadStudentsFromExcel("Students.xlsx");

                Console.WriteLine("1 - Add Student ");
                Console.WriteLine("2 - All Students ");
                Console.WriteLine("\n");

                string student_Choise = Console.ReadLine();



                /*
                 1 => اضافة طلاب 
                 */
                if (student_Choise == "1")
                {
                    Console.WriteLine(" ( * ) To Exit Write =====> end   ");

                    while (inputstudent.ToLower() == "y")
                    {

                        Console.WriteLine("\n");

                        Console.WriteLine("Student Name : ");
                        var studentname = Console.ReadLine();

                        if (studentname != "end")
                        {

                            student.Add(new Student(studentname));
                            FileManager.SaveStudentsToExcel(student, "Students.xlsx");

                        }
                        else
                        {
                            inputstudent = "n";
                        }
                    }



                    Console.WriteLine("Students saved successfully.\n");
                }
                /*
               2 => استعراض الطلاب 
               */
                else if (student_Choise == "2")
                {
                    // قراءة وعرض الطلاب من Excel
                    var allStudents = FileManager.ReadStudentsFromExcel("Students.xlsx");
                    Console.WriteLine("Students from Excel:");
                    Console.WriteLine("\n");

                    foreach (var s in allStudents)
                    {
                     
                        
                        Console.WriteLine($"Student Name => {s.Name}");




                    }
                }






            }

            #endregion
            /*3*/
            #region Assign Grade

            /* 
          3 =>  هتكون لادخال الدرجات  
           */
            else if (Return_User_Result.ToLower() == "3")
            {
                var allStudents = FileManager.ReadStudentsFromExcel("Students.xlsx");

                Student.AssignGrade(allStudents);   

                FileManager.SaveStudentsToExcel(allStudents, "Students.xlsx");

                Console.WriteLine("\nGrades saved successfully.");


            }

            #endregion
            /*4*/
            #region risk and top students?

            /* 
          4 =>  هتكون ترتيب الطلاب حسب  الدرجات  
           */
            else if (Return_User_Result.ToLower() == "4")
            {

            }
            #endregion
            /*5*/
            #region Exit

            /* 
          5 =>  للخروج 
           */
            else if (Return_User_Result.ToLower() == "5")
            {

            }
            #endregion


        }




        #region Courses



        //Console.WriteLine("Do you want to add a subject? (y/n)");
        //string input = Console.ReadLine();

        //if (input.ToLower() != "n")
        //{
        //    // إضافة مواد جديدة

        //    while (input.ToLower() == "y")
        //    {
        //        Console.WriteLine("Enter subject name:");
        //        string name = Console.ReadLine();

        //        Console.WriteLine("Enter subject hours:");
        //        int hours;

        //        while (!int.TryParse(Console.ReadLine(), out hours))
        //        {
        //            Console.WriteLine("Enter subject hours again:");
        //        }

        //        courses.Add(new Course(name, hours));

        //        Console.WriteLine("Do you want to add another subject? (y/n)");
        //        input = Console.ReadLine();
        //    }


        //    courses.AddRange(coursesData);// دمج المواد القديمة والجديدة

        //    // حفظ كل المواد في Excel
        //    FileManager.SaveCoursesToExcel(courses, "Courses.xlsx");

        //    Console.WriteLine("Courses saved successfully.\n");

        //    // عرض المواد من Excel

        //    Console.WriteLine("Courses from Excel:");
        //    var allCourses = FileManager.ReadCoursesFromExcel("Courses.xlsx");
        //    foreach (var item in allCourses)
        //    {
        //        Console.WriteLine($"Course Name: {item.Name}  Hours:{item.CreditHours}");
        //    }

        //}
        #endregion

        #region Students
        //Console.WriteLine("Do you want to add a Student? (y/n)");
        //string inputstudent = Console.ReadLine();

        //var oldStudents = FileManager.ReadStudentsFromExcel("Students.xlsx");

        //while (inputstudent.ToLower() == "y")
        //{

        //    Console.WriteLine("StudentName : ");
        //    var studentname = Console.ReadLine();


        //    student.Add(new Student(studentname));

        //    Console.WriteLine("Do you want to add another subject? (y/n)");
        //    inputstudent = Console.ReadLine();
        //}
        //FileManager.SaveStudentsToExcel(student, "Students.xlsx");
        //Console.WriteLine("Students saved successfully.\n");

        //// قراءة وعرض الطلاب من Excel
        //var allStudents = FileManager.ReadStudentsFromExcel("Students.xlsx");
        //Console.WriteLine("Students from Excel:");
        //foreach (var s in allStudents)
        //{
        //    Console.WriteLine($"Student Name: {s.Name}");
        //}

        #endregion

       #region Testing by omnia

      
        #endregion
    }
}



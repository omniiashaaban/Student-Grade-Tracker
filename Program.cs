
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
            var coursesData = FileManager.ReadCoursesFromText("Courses.txt");
            var StudentsData = FileManager.ReadStudentsFromText("Courses.txt");



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
                    Console.WriteLine(" ( * ) To Exit Write =====> end   ");

                    while (defultVAlu.ToLower() == "y")
                    {

                        Console.WriteLine("Enter subject name:");
                        string name = Console.ReadLine();

                        if (name.ToLower() != "end")
                        {
                            Console.WriteLine("Enter subject hours:");
                            int hours;

                            while (!int.TryParse(Console.ReadLine(), out hours))
                            {
                                Console.WriteLine("Enter subject hours again:");
                            }
                            courses.Add(new Course(name, hours));

                        }
                        else
                        {
                            defultVAlu = "n";
                        }

                    }

                    // دمج المواد القديمة مع الجديدة
                    courses.AddRange(coursesData);

                    // حفظ في ملف Text
                    FileManager.SaveCoursesToText(courses, "Courses.txt");

                    Console.WriteLine("Courses saved successfully.\n");
                }
                // لو كتب 2 هيعرض الكورسات  
                else if (CourseChois == "2")
                {
                    Console.WriteLine("Courses from Text File:");
                    Console.WriteLine("\n");

                    var allCourses = FileManager.ReadCoursesFromText("Courses.txt");
                    foreach (var item in allCourses)
                    {
                        Console.WriteLine($"Course Name: {item.Name}  ||  Hours: {item.CreditHours}");
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
                // قراءة الطلاب القدام
                var oldStudents = FileManager.ReadStudentsFromText("Students.txt");

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

                        if (studentname.ToLower() != "end")
                        {
                            student.Add(new Student(studentname));
                        }
                        else
                        {
                            inputstudent = "n";
                        }
                    }

                    // دمج الطلاب الجدد مع القدام
                    student.AddRange(oldStudents);

                    // حفظ في ملف Text
                    FileManager.SaveStudentsToText(student, "Students.txt");

                    Console.WriteLine("Students saved successfully.\n");
                }

                /*
                 2 => استعراض الطلاب 
                 */
                else if (student_Choise == "2")
                {
                    var allStudents = FileManager.ReadStudentsFromText("Students.txt");

                    Console.WriteLine("Students from Text File:");
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
                var allStudents = FileManager.ReadStudentsFromText("Students.txt");

                Student.AssignGrade(allStudents , coursesData);

                FileManager.SaveStudentsToText(allStudents, "Students.text");

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
                var allStudents = FileManager.ReadStudentsFromText("Students.txt");

                GradeManager.riskandtopstudents(allStudents);
            }
            #endregion
            

        }





        #region Testing by omnia


        #endregion
    }
}



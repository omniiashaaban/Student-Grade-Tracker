using OOP_Pro.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Pro
{
    internal class Program
    {
        static string Questions()
        {
            Console.WriteLine("How Can i Help You ?");
            Console.WriteLine(" 1 - Course ? ");
            Console.WriteLine(" 2 - Student ?");
            Console.WriteLine(" 3 - Assign Grade ?");
            Console.WriteLine(" 4 - Risk and top students ?");
            Console.WriteLine(" 5 - Attend Course ?");
            Console.WriteLine(" 6 - Exit");
            Console.WriteLine("\n");

            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                // Always read latest data from files
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                string coursesPath = Path.Combine(basePath, "Courses.txt");
                string studentsPath = Path.Combine(basePath, "Students.txt");
                
                var coursesData = FileManager.ReadCoursesFromText(coursesPath);
                var studentsData = FileManager.ReadStudentsFromText(studentsPath);

                string Return_User_Result = Questions();
                Console.WriteLine("\n");

                // Exit
                if (Return_User_Result == "6")
                    break;

                var defultVAlu = "y";
                var inputstudent = "y";

                #region Course
                if (Return_User_Result == "1")
                {
                    Console.WriteLine("1 - Add Course ");
                    Console.WriteLine("2 - All Courses ");
                    Console.WriteLine("\n");

                    string CourseChois = Console.ReadLine();

                    if (CourseChois == "1")
                    {
                        List<Course> newCourses = new List<Course>();

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
                                    Console.WriteLine("Enter subject hours again:");

                                Console.WriteLine("Enter Lectures Number:");
                                int lectures;
                                while (!int.TryParse(Console.ReadLine(), out lectures))
                                    Console.WriteLine("Enter Lectures Number again:");

                                newCourses.Add(new Course(name, hours, lectures));
                            }
                            else
                            {
                                defultVAlu = "n";
                            }
                        }

                        // merge old + new
                        newCourses.AddRange(coursesData);

                        FileManager.SaveCoursesToText(newCourses, coursesPath);
                        Console.WriteLine("Courses saved successfully.\n");
                        Console.ReadKey();
                    }
                    else if (CourseChois == "2")
                    {
                        Console.WriteLine("Courses from Text File:\n");
                        foreach (var item in coursesData)
                            Console.WriteLine($"Course Name: {item.Name}  ||  Hours: {item.CreditHours} || {item.NumberOfLeactures}");

                        Console.ReadKey();
                    }
                }
                #endregion

                #region Student
                else if (Return_User_Result == "2")
                {
                    Console.WriteLine("1 - Add Student ");
                    Console.WriteLine("2 - All Students ");
                    Console.WriteLine("\n");

                    string student_Choise = Console.ReadLine();

                    if (student_Choise == "1")
                    {
                        List<Student> newStudents = new List<Student>();

                        Console.WriteLine(" ( * ) To Exit Write =====> end   ");

                        while (inputstudent.ToLower() == "y")
                        {
                            Console.WriteLine("\nStudent Name : ");
                            var studentname = Console.ReadLine();

                            if (studentname.ToLower() != "end")
                                newStudents.Add(new Student(studentname));
                            else
                                inputstudent = "n";
                        }

                        // merge old + new
                        newStudents.AddRange(studentsData);

                        FileManager.SaveStudentsToText(newStudents, studentsPath);
                        Console.WriteLine("Students saved successfully.\n");
                        Console.ReadKey();
                    }
                    else if (student_Choise == "2")
                    {
                        Console.WriteLine("Students from Text File:\n");
                        foreach (var s in studentsData)
                            Console.WriteLine($"Student Name => {s.Name}");

                        Console.ReadKey();
                    }
                }
                #endregion

                #region Assign Grade
                else if (Return_User_Result == "3")
                {
                    
                    string gradesPath = Path.Combine(basePath, "Grades.txt");
                    Student.AssignGrade(studentsData, coursesData, gradesPath);

                    Console.WriteLine("\nGrades saved successfully.");
                    Console.ReadKey();
                }
                #endregion

                #region risk and top students
                else if (Return_User_Result == "4")
                {
                    GradeManager.riskandtopstudents(studentsData);
                    Console.ReadKey();
                }
                #endregion

                #region Attendance
                else if (Return_User_Result == "5")
                {
                    try
                    {
                        Console.WriteLine("1 - Take Attendance");
                        Console.WriteLine("2 - Show Attendance (One Student)");
                        Console.WriteLine("3 - Show Attendance (All Students)");
                        Console.WriteLine("\n");

                        string attChoice = Console.ReadLine();
                        string attendancePath = Path.Combine(basePath, "Attendance.txt");

                        if (attChoice == "1")
                            AttendanceManager.TakeAttendance(studentsData, coursesData, attendancePath);
                        else if (attChoice == "2")
                            AttendanceManager.ShowAttendanceForSelectedStudent(studentsData, attendancePath);
                        else if (attChoice == "3")
                            AttendanceManager.ShowAttendanceForAllStudents(studentsData, attendancePath);
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Attendance crashed بسبب:");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        Console.ReadKey();
                    }
                }

                #endregion

                Console.Clear();
            }
        }
    }
}

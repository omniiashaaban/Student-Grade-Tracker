using OOP_Pro.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Pro
{
    internal class Program
    {
        //fathi
        static string Questions()
        {
            Console.WriteLine("How Can I Help You ?");

            Console.WriteLine("1 - Course");
            Console.WriteLine("2 - Student");
            Console.WriteLine("3 - Register Student Courses");
            Console.WriteLine("4 - Assign Grade");
            Console.WriteLine("5 - Attendance");
            Console.WriteLine("6 - Risk and Top Students");
            Console.WriteLine("7 - Exit");
            Console.WriteLine();

            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string coursesPath = Path.Combine(basePath, "Courses.txt");
                string studentsPath = Path.Combine(basePath, "Students.txt");
                string gradesPath = Path.Combine(basePath, "Grades.txt");
                string attendancePath = Path.Combine(basePath, "Attendance.txt");

                var coursesData = FileManager.ReadCoursesFromText(coursesPath);
                var studentsData = FileManager.ReadStudentsFromText(studentsPath);

                string choice = Questions();

            

                if (choice == "1")
                {
                    Console.WriteLine("1 - Add Course");
                    Console.WriteLine("2 - All Courses");
                    Console.WriteLine();

                    string sub = Console.ReadLine();

                    if (sub == "1")
                    {
                        List<Course> newCourses = new List<Course>();

                        Console.WriteLine("Write (end) to stop");

                        while (true)
                        {
                            Console.Write("Course Name: ");
                            string name = Console.ReadLine();
                            if (name.ToLower() == "end") break;

                            Console.Write("Credit Hours: ");
                            int hours;
                            while (!int.TryParse(Console.ReadLine(), out hours)) ;

                            Console.Write("Lectures Count: ");
                            int lectures;
                            while (!int.TryParse(Console.ReadLine(), out lectures)) ;

                            newCourses.Add(new Course(name, hours, lectures));
                        }

                        newCourses.AddRange(coursesData);
                        FileManager.SaveCoursesToText(newCourses, coursesPath);

                        Console.WriteLine("Courses saved successfully.");
                        Console.ReadKey();
                    }
                    else if (sub == "2")
                    {
                        foreach (var c in coursesData)
                            Console.WriteLine($"{c.Name} | Hours: {c.CreditHours} | Lectures: {c.NumberOfLeactures}");

                        Console.ReadKey();
                    }
                }

                #region Student

                else if (choice == "2")
                {
                    Console.WriteLine("1 - Add Student");
                    Console.WriteLine("2 - All Students");
                    Console.WriteLine();

                    string sub = Console.ReadLine();

                    if (sub == "1")
                    {
                        List<Student> newStudents = new List<Student>();
                        Console.WriteLine("Write (end) to stop");

                        while (true)
                        {
                            Console.Write("Student Name: ");
                            string name = Console.ReadLine();
                      
                            if (name.ToLower() == "end") break;

                            newStudents.Add(new Student(name));
                        }

                        newStudents.AddRange(studentsData);
                        FileManager.SaveStudentsToText(newStudents, studentsPath);

                        Console.WriteLine("Students saved successfully.");
                        Console.ReadKey();
                    }
                    else if (sub == "2")
                    {
                        foreach (var s in studentsData)
                            Console.WriteLine(s.Name);

                        Console.ReadKey();
                    }
                }
                #endregion
// fathi 
                #region Register Courses

                else if (choice == "3")
                {
                    if (studentsData.Count == 0 || coursesData.Count == 0)
                    {
                        Console.WriteLine("Students or Courses not found.");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Choose Student:");
                    for (int i = 0; i < studentsData.Count; i++)
                        Console.WriteLine($"{i + 1}- {studentsData[i].Name}");

                    if (!int.TryParse(Console.ReadLine(), out int idx) ||
                        idx < 1 || idx > studentsData.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        continue;
                    }

                    Student student = studentsData[idx - 1];

                    FileManager.RegisterCoursesFromFile(student, coursesPath);

                    FileManager.SaveStudentGradesToText(gradesPath, student);

                    //Console.WriteLine("Courses registered successfully.");
                    Console.ReadKey();
                }
                #endregion
// omnia
                #region Assign Grade

                else if (choice == "4")
                {
                    Console.WriteLine("1 - One Student");
                    Console.WriteLine("2 - All Students");
               var Co=      Console.ReadLine();
                    if (Co == "1")
                    {
                        Student.AssignGradeForOneStudent(studentsData, coursesData, gradesPath);

                    }
                    else if (Co == "2")
                            {
                                Student.AssignGrade(studentsData, coursesData, gradesPath);
                                Console.WriteLine("Grades saved.");
                                Console.ReadKey();
                            }
                  
                }
                #endregion
 // islam 

                #region Attendance
                else if (choice == "5")
                {
                    Console.WriteLine("1 - Take Attendance");
                    Console.WriteLine("2 - Show Attendance (One Student)");
                    Console.WriteLine("3 - Show Attendance (All Students)");

                    string sub = Console.ReadLine();

                    if (sub == "1")
                        AttendanceManager.TakeAttendance(studentsData, coursesData, attendancePath);
                    else if (sub == "2")
                        AttendanceManager.ShowAttendanceForSelectedStudent(studentsData, attendancePath);
                    else if (sub == "3")
                        AttendanceManager.ShowAttendanceForAllStudents(studentsData, attendancePath);

                    Console.ReadKey();
                }
                #endregion
// fathi 
                #region Risk & Top
                else if (choice == "6")
                {
                    GradeManager.riskandtopstudents(studentsData);
                    Console.ReadKey();
                }
                #endregion
    if (choice == "7")
                    break;
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
 

namespace OOP_Pro.Models
{
   
    public static class AttendanceManager
    {
        public static void TakeAttendance(List<Student> students, List<Course> courses, string attendancePath)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            if (courses == null || courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                Console.ReadKey();
                return;
            }

            // Select Student 
            Console.WriteLine("\nSelect Student:");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}- {students[i].Name}");

            int studentIndex = ReadIntInRange("Enter student number: ", 1, students.Count) - 1;
            Student student = students[studentIndex];

            if (student.Courses == null)
                student.Courses = new List<StudentCourse>();

            // Load existing attendance so we continue incrementing correctly
            FileManager.LoadStudentAttendanceFromText(attendancePath, student);

            //  Select Course 
            Console.WriteLine($"\nSelect Course for {student.Name}:");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i + 1}- {courses[i].Name} (Total Lectures: {courses[i].NumberOfLeactures})");

            int courseIndex = ReadIntInRange("Enter course number: ", 1, courses.Count) - 1;
            Course selectedCourse = courses[courseIndex];

            //  Find course inside student 
            StudentCourse studentCourse = student.Courses.FirstOrDefault(sc =>
                sc.Course.Name.Trim().Equals(selectedCourse.Name.Trim(), StringComparison.OrdinalIgnoreCase));

            // if not found, create it
            if (studentCourse == null)
            {
                studentCourse = new StudentCourse(selectedCourse, 0);
                studentCourse.NumberOfLeacturesAttended = 0; 
                student.Courses.Add(studentCourse);
            }
            else
            {
                studentCourse.Course = selectedCourse;
            }

            // Increment Attendance with cap 
            if (studentCourse.NumberOfLeacturesAttended >= selectedCourse.NumberOfLeactures)
            {
                Console.WriteLine("Attendance already completed for this course.");
                Console.ReadKey();
                return;
            }

            studentCourse.NumberOfLeacturesAttended++;
            
            Console.WriteLine(
                $"Attendance added: {studentCourse.NumberOfLeacturesAttended}/{selectedCourse.NumberOfLeactures} " +
                $"for {selectedCourse.Name}"
            );

            FileManager.SaveStudentAttendanceToText(attendancePath, student);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void ShowAttendanceForSelectedStudent(List<Student> students, string attendancePath)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nSelect Student:");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}- {students[i].Name}");

            int studentIndex = ReadIntInRange("Enter student number: ", 1, students.Count) - 1;
            Student student = students[studentIndex];

            if (student.Courses == null)
                student.Courses = new List<StudentCourse>();

            FileManager.LoadStudentAttendanceFromText(attendancePath, student);

            Console.WriteLine($"\nAttendance Report for {student.Name}:\n");

            var attendedCourses = student.Courses.Where(sc => sc.NumberOfLeacturesAttended > 0).ToList();
            if (attendedCourses.Count == 0)
            {
                Console.WriteLine("No attendance records.");
                Console.ReadKey();
                return;
            }

            foreach (var sc in attendedCourses)
            {
                Console.WriteLine($"{sc.Course.Name}: {sc.NumberOfLeacturesAttended}/{sc.Course.NumberOfLeactures}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void ShowAttendanceForAllStudents(List<Student> students, string attendancePath)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAll Students Attendance:\n");

            foreach (var student in students)
            {
                if (student.Courses == null)
                    student.Courses = new List<StudentCourse>();

                FileManager.LoadStudentAttendanceFromText(attendancePath, student);

                Console.WriteLine($"Student: {student.Name}");

                var attendedCourses = student.Courses.Where(sc => sc.NumberOfLeacturesAttended > 0).ToList();
                if (attendedCourses.Count == 0)
                {
                    Console.WriteLine("  - No attendance records.\n");
                    continue;
                }

                foreach (var sc in attendedCourses)
                {
                    Console.WriteLine($"  - {sc.Course.Name}: {sc.NumberOfLeacturesAttended}/{sc.Course.NumberOfLeactures}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static int ReadIntInRange(string message, int min, int max)
        {
            int value;
            Console.Write(message);

            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write($"Invalid input. Enter a number between {min} and {max}: ");
            }

            return value;
        }
    }

}

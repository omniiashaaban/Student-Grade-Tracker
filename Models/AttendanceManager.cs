using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Pro.Models
{
    public static class AttendanceManager
    {
        public static void TakeAttendance(List<Student> students, List<Course> courses, string attendancePath)
        {
            if (students == null || students.Count == 0 || courses == null || courses.Count == 0)
            {
                Console.WriteLine("No data available.");
                Console.ReadKey();
                return;
            }

            Student student = SelectStudent(students);
            Course course = SelectCourse(courses);

            if (student.Courses == null)
                student.Courses = new List<StudentCourse>();

            FileManager.LoadStudentAttendanceFromText(attendancePath, student);

            var studentCourse = student.Courses
                .FirstOrDefault(sc => sc.Course.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase));

            if (studentCourse == null)
            {
                studentCourse = new StudentCourse(course, 0);
                student.Courses.Add(studentCourse);
            }

            if (studentCourse.NumberOfLeacturesAttended >= course.NumberOfLeactures)
            {
                Console.WriteLine("Attendance already completed for this course.");
                Console.ReadKey();
                return;
            }

            studentCourse.NumberOfLeacturesAttended++;

            FileManager.SaveStudentAttendanceToText(attendancePath, student);

            Console.WriteLine(
                $"Attendance saved: {studentCourse.NumberOfLeacturesAttended}/{course.NumberOfLeactures} - {course.Name}"
            );

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

            Student student = SelectStudent(students);

            if (student.Courses == null)
                student.Courses = new List<StudentCourse>();

            FileManager.LoadStudentAttendanceFromText(attendancePath, student);

            Console.WriteLine($"\nAttendance for {student.Name}:");

            var courses = student.Courses.Where(c => c.NumberOfLeacturesAttended > 0).ToList();

            if (!courses.Any())
            {
                Console.WriteLine("No attendance records.");
                Console.ReadKey();
                return;
            }

            foreach (var c in courses)
                Console.WriteLine($"{c.Course.Name}: {c.NumberOfLeacturesAttended}/{c.Course.NumberOfLeactures}");

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

            foreach (var student in students)
            {
                if (student.Courses == null)
                    student.Courses = new List<StudentCourse>();

                FileManager.LoadStudentAttendanceFromText(attendancePath, student);

                Console.WriteLine($"\nStudent: {student.Name}");

                var courses = student.Courses.Where(c => c.NumberOfLeacturesAttended > 0).ToList();

                if (!courses.Any())
                {
                    Console.WriteLine("No attendance records.");
                    continue;
                }

                foreach (var c in courses)
                {
                    double percentage =
                        c.Course.NumberOfLeactures == 0
                        ? 0
                        : (double)c.NumberOfLeacturesAttended / c.Course.NumberOfLeactures * 100;

                    Console.WriteLine(
                        $"{c.Course.Name}: {c.NumberOfLeacturesAttended}/{c.Course.NumberOfLeactures} ({percentage:F1}%)"
                    );
                }
            }

            Console.ReadKey();
        }

        private static Student SelectStudent(List<Student> students)
        {
            Console.WriteLine("\nSelect Student:");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}- {students[i].Name}");

            int index = ReadInt( 1, students.Count) - 1;
            return students[index];
        }

        private static Course SelectCourse(List<Course> courses)
        {
            Console.WriteLine("\nSelect Course:");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i + 1}- {courses[i].Name}");

            int index = ReadInt( 1, courses.Count) - 1;
            return courses[index];
        }

        private static int ReadInt( int min, int max)
        {
            int value;
            Console.Write("Choose number: ");


            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
                Console.Write($"Enter number between {min} and {max}: ");

            return value;
        }
    }
}

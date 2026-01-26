using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OOP_Pro.Models
{
    public class FileManager
    {
        #region Courses

        public static List<Course> ReadCoursesFromText(string filePath)
        {
            List<Course> courses = new List<Course>();

            if (!File.Exists(filePath))
                return courses;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');

                // validation مهم
                if (parts.Length < 3)
                    continue; // أو throw exception لو حابب

                string name = parts[0];

                if (!int.TryParse(parts[1], out int creditHours))
                    continue;

                if (!int.TryParse(parts[2], out int numberOFLeactures))
                    continue;

                courses.Add(new Course(name, creditHours, numberOFLeactures));
            }


            return courses;
        }
        public static void SaveCoursesToText(List<Course> courses, string filePath)
        {
            List<string> lines = new List<string>();

            foreach (var c in courses)
            {

                lines.Add($"{c.Name}|{c.CreditHours}|{c.NumberOfLeactures}");
                //lines.Add((s) new Course(c.Name, c.CreditHours, c.NumberOfLeactures));

            }

            File.WriteAllLines(filePath, lines);
        }

        #endregion


        #region Students

        public static List<Student> ReadStudentsFromText(string filePath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(filePath))
                return students;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    students.Add(new Student(line.Trim()));
                }
            }

            return students;
        }
        public static void SaveStudentsToText(List<Student> students, string filePath)
        {
            List<string> lines = new List<string>();

            foreach (var s in students)
            {
                lines.Add(s.Name);
            }

            File.WriteAllLines(filePath, lines);
        }

        #endregion

        #region StudentGrade
        public static void SaveStudentGradesToText(string filePath, Student student)
        {
            var allLines = new List<string>();

            // read existing
            if (File.Exists(filePath))
                allLines = File.ReadAllLines(filePath).ToList();

            // remove old records for this student
            allLines = allLines
                .Where(l => !l.StartsWith(student.Name + "|", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // add updated records
            foreach (var sc in student.Courses)
            {
                allLines.Add($"{student.Name}|{sc.Course.Name}|{sc.Course.CreditHours}|{sc.Course.NumberOfLeactures}|{sc.Grade}");
            }

            File.WriteAllLines(filePath, allLines);
        }

        public static void LoadStudentGradesFromText(string filePath, Student student)
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            // don't clear -> preserve attendance
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length < 5)
                    continue; // avoid crash

                string studentName = parts[0];
                string courseName = parts[1];

                if (!studentName.Equals(student.Name, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (!int.TryParse(parts[2], out int hours)) hours = 0;
                if (!int.TryParse(parts[3], out int lectures)) lectures = 0;
                if (!double.TryParse(parts[4], out double grade)) grade = 0;

                var existing = student.Courses.FirstOrDefault(sc =>
                    sc.Course.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                {
                    existing.Course.CreditHours = hours;
                    existing.Course.NumberOfLeactures = lectures;
                    existing.Grade = grade;
                }
                else
                {
                    var course = new Course(courseName, hours, lectures);
                    student.Courses.Add(new StudentCourse(course, grade));
                }
            }
        }
        #endregion

        #region Student Attendance 


        public static void SaveStudentAttendanceToText(string filePath, Student student)
        {
            List<string> lines = new List<string>();

            if (File.Exists(filePath))
                lines = File.ReadAllLines(filePath).ToList();

            // remove old records for this student
            lines = lines
                .Where(l => !l.StartsWith(student.Name.Trim() + "|", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // save only attended courses (optional but recommended)
            foreach (var sc in student.Courses.Where(x => x.NumberOfLeacturesAttended > 0))
            {
                lines.Add($"{student.Name.Trim()}|{sc.Course.Name.Trim()}|{sc.NumberOfLeacturesAttended}");
            }

            File.WriteAllLines(filePath, lines);
        }



        public static void LoadStudentAttendanceFromText(string filePath, Student student, List<Course> allCourses)
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length < 3)
                    continue;

                string studentName = parts[0].Trim();
                string courseName = parts[1].Trim();

                if (!int.TryParse(parts[2], out int attendedLectures))
                    continue;

                if (!studentName.Equals(student.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                    continue;

                var studentCourse = student.Courses.FirstOrDefault(sc =>
                    sc.Course.Name.Trim().Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (studentCourse != null)
                {
                    studentCourse.NumberOfLeacturesAttended = attendedLectures;
                }
                else
                {
                    // البحث عن الكورس الأصلي في كل الكورسات
                    var actualCourse = allCourses.FirstOrDefault(c =>
                        c.Name.Trim().Equals(courseName, StringComparison.OrdinalIgnoreCase));

                    if (actualCourse != null)
                    {
                        var sc = new StudentCourse(actualCourse, 0)
                        {
                            NumberOfLeacturesAttended = attendedLectures
                        };
                        student.Courses.Add(sc);
                    }
                    else
                    {
                        // fallback لو مش لاقي الكورس
                        var sc = new StudentCourse(new Course(courseName, 0, 0), 0)
                        {
                            NumberOfLeacturesAttended = attendedLectures
                        };
                        student.Courses.Add(sc);
                    }
                }
            }
        }
        #endregion


        #region تسجبل الطلاب في المواد 
        public static void RegisterCoursesFromFile(Student student, string coursesPath)
        {
            List<Course> allCourses = FileManager.ReadCoursesFromText(coursesPath);

            if (allCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine($"\n Student: {student.Name}");
            Console.WriteLine("Choose courses:");

            for (int i = 0; i < allCourses.Count; i++)
            {
                Console.WriteLine($"{i + 1}- {allCourses[i].Name} " + $"(Hours: {allCourses[i].CreditHours}, Lectures: {allCourses[i].NumberOfLeactures})");
            }

            Console.WriteLine("Enter course numbers :");
            string input = Console.ReadLine();


            string[] parts = input.Split(',');

            List<int> selectedIndexes = new List<int>();

            foreach (var item in parts)
            {
                if (int.TryParse(item.Trim(), out int num))
                {
                    int index = num - 1;

                    if (index >= 0 && index < allCourses.Count)
                    {
                        selectedIndexes.Add(index);
                    }
                }
            }

            if (selectedIndexes.Count == 0)
            {
                Console.WriteLine("No valid courses selected.");
                return;
            }

            int addedCount = 0;

            foreach (int i in selectedIndexes)
            {
                Course course = allCourses[i];

                bool alreadyAdded = student.Courses
                    .Any(sc => sc.Course.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase));

                if (!alreadyAdded)
                {
                    student.Courses.Add(new StudentCourse(course, 0));
                    addedCount++;
                }
            }

            Console.WriteLine($"\n{addedCount} course(s) registered successfully!");
        }
        #endregion

    }
}

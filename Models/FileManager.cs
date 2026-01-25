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



        public static void LoadStudentAttendanceFromText(string filePath, Student student)
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
                    // fallback if not found (course details unknown here)
                    var course = new Course(courseName, 0, 0);
                    var sc = new StudentCourse(course, 0)
                    {
                        NumberOfLeacturesAttended = attendedLectures
                    };
                    student.Courses.Add(sc);
                }
            }
        }
        #endregion

    }
}

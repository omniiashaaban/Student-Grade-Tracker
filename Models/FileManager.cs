using System;
using System.Collections.Generic;
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

                string name = parts[0];
                int creditHours = int.Parse(parts[1]);

                courses.Add(new Course(name, creditHours));
            }

            return courses;
        }
        public static void SaveCoursesToText(List<Course> courses, string filePath)
        {
            List<string> lines = new List<string>();

            foreach (var c in courses)
            {
                lines.Add($"{c.Name}|{c.CreditHours}");
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
        public static void SaveStudentGradesToText(
    string filePath,
    Student student)
        {
            List<string> lines = new List<string>();

            foreach (var sc in student.Courses)
            {
                lines.Add($"{student.Name}|{sc.Course.Name}|{sc.Course.CreditHours}|{sc.Grade}");
            }

            File.AppendAllLines(filePath, lines);
        }

        public static void LoadStudentGradesFromText(string filePath, Student student)
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            student.Courses.Clear();

            foreach (var line in lines)
            {
                var parts = line.Split('|');

                string studentName = parts[0];
                string courseName = parts[1];
                int hours = int.Parse(parts[2]);
                double grade = double.Parse(parts[3]);

                if (studentName == student.Name)
                {
                    var course = new Course(courseName, hours);
                    student.Courses.Add(new StudentCourse(course, grade));
                }
            }
        }
        #endregion

    }
}

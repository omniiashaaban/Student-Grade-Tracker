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



    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OOP_Pro.Models
{
    public class Student
    {
        private static int _counter = 1; // عداد للـ ID

        public int Id { get; private set; }
        public string Name { get; set; } // اسم الطالب 

        public List<StudentCourse> Courses;     // اسماء الكورسات 
        public Student(string name)
        {
            Id = _counter++;        // توليد ID تلقائي

            Name = name;
            Courses = new List<StudentCourse>();  
        }



        #region Assign grades 
        public static void AssignGrade(List<Student> students, string gradesPath)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"\nStudent: {student.Name}\n");

               
                FileManager.LoadStudentGradesFromText(gradesPath, student);

                if (student.Courses.Count == 0)
                {
                    Console.WriteLine("This student has no courses registered.");
                    continue;
                }

                // إدخال الدرجات لكل مادة  للطالب
                for (int i = 0; i < student.Courses.Count; i++)
                {
                    var sc = student.Courses[i];

                    Console.Write($"Enter grade for {sc.Course.Name}: ");
                    double grade;
                    while (!double.TryParse(Console.ReadLine(), out grade))
                    {
                        Console.Write("Invalid input. Enter a number: ");
                    }

                    sc.Grade = grade; // تحديث الدرجة
                }

                // حفظ درجات الطالب بعد التعديل
                FileManager.SaveStudentGradesToText(gradesPath, student);

                Console.WriteLine($"Grades updated for {student.Name} .");

            
            }
        }
        #endregion



        #region Assign courses
        public static void RegisterCoursesFromFile(Student student, string coursesPath)
        {
         
            List<Course> allCourses = FileManager.ReadCoursesFromText(coursesPath);

            if (allCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

           
            Console.WriteLine($"\nStudent: {student.Name}");
            Console.WriteLine("choose courses :");

            for (int i = 0; i < allCourses.Count; i++)
            {
                Console.WriteLine($"{i + 1}- {allCourses[i].Name}");
            }

            Console.WriteLine("Enter the numbers of courses you want to register (comma separated):");
            string input = Console.ReadLine();

            string[] parts = input.Split(',');    //استرينج عشان اعمل اسبليت  
            List<int> selectedIndexes = new List<int>();

            for (int i = 0; i < parts.Length; i++)
            {
                int index;
                if (int.TryParse(parts[i].Trim(), out index))
                {
                    index = index - 1; // المستخدم يدخل الأرقام من 1
                    if (index >= 0 && index < allCourses.Count)
                    {
                        selectedIndexes.Add(index);
                    }
                }
            }

            // إضافة المواد للطالب لو مش مسجلة قبل كده
            for (int i = 0; i < selectedIndexes.Count; i++)
            {
                Course course = allCourses[selectedIndexes[i]];
                bool alreadyAdded = false;

                for (int j = 0; j < student.Courses.Count; j++)
                {
                    if (student.Courses[j].Course.Name == course.Name)
                    {
                        alreadyAdded = true;
                        break;
                    }
                }

                if (!alreadyAdded)
                {
                    student.Courses.Add(new StudentCourse(course, 0));
                }
            }

            Console.WriteLine("\nCourses registered successfully!");
        }
        #endregion




        public override string ToString()
        {
            string coursesInfo;

            if (Courses.Count == 0) coursesInfo = "No courses enrolled";
            else coursesInfo = string.Join(", ", Courses);

            return $"Name: {Name}, Courses: [{coursesInfo}]";
        }
    }
}

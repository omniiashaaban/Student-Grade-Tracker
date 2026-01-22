using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
      public class FileManager
    {
        #region Courses

        public static List<Course> ReadCoursesFromExcel(string filePath)
        {
            List<Course> courses = new List<Course>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string name = worksheet.Cells[row, 1].Text;
                    int creditHours = int.Parse(worksheet.Cells[row, 2].Text);

                    courses.Add(new Course(name, creditHours));
                }
            }

            return courses;
        }

        // تسجيل اللبيانات في شيت اكسل 

        public static void SaveCoursesToExcel(List<Course> courses, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Courses");

                // Header
                sheet.Cells[1, 1].Value = "Course Name";
                sheet.Cells[1, 2].Value = "Credit Hours";

                int row = 2;
                foreach (var c in courses)
                {
                    sheet.Cells[row, 1].Value = c.Name;
                    sheet.Cells[row, 2].Value = c.CreditHours;
                    row++;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
        #endregion

        #region Students
        public static List<Student> ReadStudentsFromExcel(string filePath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Excel file not found.");
                return students;
            }

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rows = sheet.Dimension.Rows;

                for (int i = 2; i <= rows; i++)
                {
                    string name = sheet.Cells[i, 1].Text;
                    if (!string.IsNullOrWhiteSpace(name))
                        students.Add(new Student(name));
                }
            }

            return students;
        }



 public static void SaveStudentsToExcel(List<Student> students, string filePath)
{
    FileInfo fileInfo = new FileInfo(filePath);

    using (ExcelPackage package = fileInfo.Exists
        ? new ExcelPackage(fileInfo)
        : new ExcelPackage())
    {
        ExcelWorksheet sheet;

        if (fileInfo.Exists && package.Workbook.Worksheets.Count > 0)
        {
            sheet = package.Workbook.Worksheets[0];
        }
        else
        {
            sheet = package.Workbook.Worksheets.Add("Students");
            sheet.Cells[1, 1].Value = "Student Name";
        }

        // آخر صف فيه بيانات
        int row = sheet.Dimension?.End.Row + 1 ?? 2;

        foreach (var s in students)
        {
            sheet.Cells[row, 1].Value = s.Name;
            row++;
        }

        package.SaveAs(fileInfo);
    }
}

        #endregion


    }
}

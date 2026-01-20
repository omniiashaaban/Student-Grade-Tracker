using OOP_Pro.Models;
using System;
using System.IO;

namespace OOP_Pro.Classes
{
    static class TextReading
    {
        /*
 الكلاس ده بيقرا البيانات الموجوده في ملف TEXT 
الملف بيكون محفوظ في ملف ال bin 
اسم الملف students.txt
بيقرا البيانات و بيحفظها في Arry

        Features : 
        load student data


        By Fathi 
 */
        //public static Student[] ReadStudentsFromFile(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine("File not found!");
        //        return new Student[0];
        //    }

        //    string[] lines = File.ReadAllLines(filePath);
        //    Student[] students = new Student[lines.Length];

        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        string[] data = lines[i].Split(',');

        //        students[i] = new Student
        //        {
        //            Id = int.Parse(data[0]),
        //            Name = data[1],
        //            Course = data[2],
        //            Grade = int.Parse(data[3]),
        //            Attendance = int.Parse(data[4])
        //        };
        //    }

        //    return students;
        //}
    }
}

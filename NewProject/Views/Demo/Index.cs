using System;
using NewProject.Models;

namespace NewProject.Views.Demo
{
    class Index
    {
        public static void Render(Student student)
        {
            Console.WriteLine($"Hello {student.FullName} - {student.StudentId}");
        }
    }
}
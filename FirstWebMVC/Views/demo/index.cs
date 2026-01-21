using System;
using DemoMVC.Models;

namespace DemoMVC.Views.Demo
{
    class Index
    {
        public static void Render(Student student)
        {
            Console.WriteLine($"Hello {student.FullName} - {student.StudentId}");
        }
    }
}
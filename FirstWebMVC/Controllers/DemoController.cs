using DemoMVC.Models;
using DemoMVC.Views.Demo;

namespace DemoMVC.Controllers
{
    class DemoController
    {
        public void Index()
        {
            Student sv = new Student
            {
                FullName = "Vũ Văn Cường",
                StudentId = "2221050692"
            };

            Index.Render(sv);
        }
    }
}
using NewProject.Models;

namespace NewProject.Controllers
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

            Views.Demo.Index.Render(sv);
        }
    }
}
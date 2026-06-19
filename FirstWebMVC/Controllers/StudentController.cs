using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                ViewBag.Message = "Bạn chưa nhập họ tên.";
            }
            else
            {
                ViewBag.Message = $"Xin chào {student.FullName}";
            }

            return View(student);
        }
    }
}

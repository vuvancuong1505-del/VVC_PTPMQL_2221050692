using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Student;
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
        public IActionResult Index(Student std)
        {
            ViewBag.ThongBao = "Xin chào: " + std.FullName + " - Mã sinh viên: " + std.StudentCode;
            return View();
        }
    }
}
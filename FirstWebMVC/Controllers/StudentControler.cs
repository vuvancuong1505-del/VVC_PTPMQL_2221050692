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
        public IActionResult Index(string StudentCode, string FullName)
        {
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;

            if(string.IsNullOrWhiteSpace(StudentCode) && string.IsNullOrWhiteSpace(FullName)){
                ViewBag.Message = "Vui lòng nhập thông tin của bạn";
                ViewBag.Error = true;
                return View();
            }
            else if (string.IsNullOrWhiteSpace(StudentCode)){
                ViewBag.Message = "Vui lòng nhập mã sinh viên của bạn";
                ViewBag.Error = true;
                return View();
            }
            else if (string.IsNullOrWhiteSpace(FullName)){
                ViewBag.Message = "Vui lòng nhập họ và tên của bạn";
                ViewBag.Error = true;
                return View();
            }
            else{
                ViewBag.Message = "Xin chào: " + FullName + " - Mã sinh viên: " + StudentCode;
                ViewBag.Error = false;
                return View();
            }
        }
    }
}
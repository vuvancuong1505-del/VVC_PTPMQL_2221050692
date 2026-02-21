using Microsoft.AspNetCore.Mvc;

namespace FirstWebMVC.Controllers
{
     public class DemoController : Controller
     {
            public IActionResult Index()
            {
                //Sử dụng Viewbag để gửi dữ liệu từ Controller về View
                ViewBag.FullName = "Vũ Văn Cường - 2221050692";
                return View();
            }
     }
}

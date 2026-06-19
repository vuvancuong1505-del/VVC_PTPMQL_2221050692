using Microsoft.AspNetCore.Mvc;

namespace FirstWebMVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello Vũ Văn Cường - 2221050692";
            return View();
        }
    }
}

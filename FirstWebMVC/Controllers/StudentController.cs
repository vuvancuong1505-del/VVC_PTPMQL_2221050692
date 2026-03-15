using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Student;
using FirstWebMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var student = await _context.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            _context.Students.Update(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}





// namespace FirstWebMVC.Controllers
// {
//     public class StudentController : Controller
//     {
//         [HttpGet]
//         public IActionResult Index()
//         {
//             return View();
//         }
//         [HttpPost]
//         public IActionResult Index(string StudentCode, string FullName)
//         {
//             ViewBag.StudentCode = StudentCode;
//             ViewBag.FullName = FullName;

//             if(string.IsNullOrWhiteSpace(StudentCode) && string.IsNullOrWhiteSpace(FullName)){
//                 ViewBag.Message = "Vui lòng nhập thông tin của bạn";
//                 ViewBag.Error = true;
//                 return View();
//             }
//             else if (string.IsNullOrWhiteSpace(StudentCode)){
//                 ViewBag.Message = "Vui lòng nhập mã sinh viên của bạn";
//                 ViewBag.Error = true;
//                 return View();
//             }
//             else if (string.IsNullOrWhiteSpace(FullName)){
//                 ViewBag.Message = "Vui lòng nhập họ và tên của bạn";
//                 ViewBag.Error = true;
//                 return View();
//             }
//             else{
//                 ViewBag.Message = "Xin chào: " + FullName + " - Mã sinh viên: " + StudentCode;
//                 ViewBag.Error = false;
//                 return View();
//             }
//         }
//     }
// }
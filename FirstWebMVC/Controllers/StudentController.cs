using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Student;
using FirstWebMVC.Models.ViewModels;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Student
        public async Task<IActionResult> Index()
        {
            // var students = await _context.Students.ToListAsync();
            // return View(students);
            var result = await _context.Students
                            .Select(s => new StudentVM
                            {
                                StudentCode = s.StudentCode,
                                FullName = s.FullName,
                                FacultyName = s.Faculty!.FacultyName
                            })
                            .ToListAsync();
            return View(result);
        }

        // GET: Student/Details/5
         public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        
        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(Student std)
        public async Task<IActionResult> Create([Bind("StudentCode,FullName,FacultyId")] Student student)
        {
            // if (ModelState.IsValid)
            // {
            //     _context.Students.Add(std);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
            // return View(std);
            if (ModelState.IsValid)
            {
                if (StudentExists(student.StudentCode))
                {
                    ModelState.AddModelError("StudentCode", "Ma sinh vien da ton tai");
                    return View(student);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        
        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            // var student = await _context.Students.FindAsync(id);
            // return View(student);
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }


        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(Student std)
        public async Task<IActionResult> Edit(string id, [Bind("StudentCode,FullName,FacultyId")] Student student)
        {

            // if (ModelState.IsValid)
            // {
            //     _context.Students.Update(std);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
            // return View(std);
            if (id != student.StudentCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        
        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            // if (id == null)
            // {
            //     return View("NotFound");
            // }
            
            // var student = await _context.Students.FindAsync(id);

            // if (student == null)
            // {
            //     return View("NotFound");
            // }

            // return View(student);
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        

        // POST: Student/Delete/5
        // [HttpPost]
        // [ActionName("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // var student = await _context.Students.FindAsync(id);

            // if (student != null)
            // {
            //     _context.Students.Remove(student);
            //     await _context.SaveChangesAsync();
            // }

            // return RedirectToAction(nameof(Index));
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
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
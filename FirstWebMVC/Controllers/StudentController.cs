using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using FirstWebMVC.ViewModels;

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
            var students = await _context.Students
                .Include(s => s.Faculty)
                .OrderBy(s => s.StudentCode)
                .Select(s => new StudentFacultyViewModel
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    Email = s.Email,
                    Age = s.Age,
                    FacultyName = s.Faculty != null ? s.Faculty.Name : string.Empty
                })
                .ToListAsync();

            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateFacultiesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateFacultiesAsync();
            return View(student);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await PopulateFacultiesAsync(student.FacultyId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            if (id != student.StudentCode)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateFacultiesAsync(student.FacultyId);
                return View(student);
            }

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
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(s => s.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
        }

        private async Task PopulateFacultiesAsync(int? selectedFacultyId = null)
        {
            var faculties = await _context.Faculties.OrderBy(d => d.Name).ToListAsync();
            ViewBag.Faculties = new SelectList(faculties, "FacultyId", "Name", selectedFacultyId);
        }
    }
}

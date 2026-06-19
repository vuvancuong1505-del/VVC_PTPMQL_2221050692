using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OfficeOpenXml;
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

        public IActionResult Upload()
        {
            return View(new StudentImportResultViewModel());
        }

        public IActionResult DownloadTemplate()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students");
            worksheet.Cells[1, 1].Value = "StudentCode";
            worksheet.Cells[1, 2].Value = "FullName";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "Age";
            worksheet.Cells[1, 5].Value = "FacultyName";

            worksheet.Cells[2, 1].Value = "SV001";
            worksheet.Cells[2, 2].Value = "Nguyễn Văn A";
            worksheet.Cells[2, 3].Value = "nguyenvana@example.com";
            worksheet.Cells[2, 4].Value = "20";
            worksheet.Cells[2, 5].Value = "Công nghệ thông tin";

            worksheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
            worksheet.Cells.AutoFitColumns();

            var fileContents = package.GetAsByteArray();
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentTemplate.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = new StudentImportResultViewModel();

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Vui lòng chọn file Excel.");
                return View(result);
            }

            using var stream = file.OpenReadStream();
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                ModelState.AddModelError("file", "File Excel không chứa sheet nào.");
                return View(result);
            }

            var existingFacultyMap = await _context.Faculties.ToDictionaryAsync(f => f.Name.Trim(), f => f.FacultyId);
            var startRow = 2;
            var lastRow = worksheet.Dimension?.End.Row ?? 1;

            for (var row = startRow; row <= lastRow; row++)
            {
                result.TotalRows++;

                var studentCode = worksheet.Cells[row, 1].Text.Trim();
                var fullName = worksheet.Cells[row, 2].Text.Trim();
                var email = worksheet.Cells[row, 3].Text.Trim();
                var ageText = worksheet.Cells[row, 4].Text.Trim();
                var facultyName = worksheet.Cells[row, 5].Text.Trim();

                if (string.IsNullOrEmpty(studentCode) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(ageText) || string.IsNullOrEmpty(facultyName))
                {
                    result.SkippedCount++;
                    result.Errors.Add($"Dòng {row}: Thiếu thông tin bắt buộc.");
                    continue;
                }

                if (!int.TryParse(ageText, out var age) || age < 16 || age > 100)
                {
                    result.SkippedCount++;
                    result.Errors.Add($"Dòng {row}: Tuổi không hợp lệ.");
                    continue;
                }

                if (!new EmailAddressAttribute().IsValid(email))
                {
                    result.SkippedCount++;
                    result.Errors.Add($"Dòng {row}: Email không hợp lệ.");
                    continue;
                }

                if (!existingFacultyMap.TryGetValue(facultyName, out var facultyId))
                {
                    var faculty = new Faculty { Name = facultyName };
                    _context.Faculties.Add(faculty);
                    await _context.SaveChangesAsync();
                    facultyId = faculty.FacultyId;
                    existingFacultyMap[facultyName] = facultyId;
                }

                if (await _context.Students.AnyAsync(s => s.StudentCode == studentCode))
                {
                    result.SkippedCount++;
                    result.Errors.Add($"Dòng {row}: Mã sinh viên '{studentCode}' đã tồn tại.");
                    continue;
                }

                var student = new Student
                {
                    StudentCode = studentCode,
                    FullName = fullName,
                    Email = email,
                    Age = age,
                    FacultyId = facultyId
                };

                _context.Students.Add(student);
                result.CreatedCount++;
            }

            if (result.CreatedCount > 0)
            {
                await _context.SaveChangesAsync();
            }

            return View(result);
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

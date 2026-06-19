using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using FirstWebMVC.ViewModels;

namespace FirstWebMVC.Controllers
{
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var donHangs = await _context.DonHangs
                .Include(d => d.Student)
                .OrderBy(d => d.NgayDat)
                .Select(d => new DonHangViewModel
                {
                    DonHangId = d.DonHangId,
                    MaDonHang = d.MaDonHang,
                    NgayDat = d.NgayDat,
                    TongTien = d.TongTien,
                    StudentCode = d.StudentCode,
                    StudentName = d.Student != null ? d.Student.FullName : string.Empty
                })
                .ToListAsync();

            return View(donHangs);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateStudentsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.DonHangs.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateStudentsAsync(donHang.StudentCode);
            return View(donHang);
        }

        private async Task PopulateStudentsAsync(string? selectedStudentCode = null)
        {
            var students = await _context.Students.OrderBy(s => s.FullName).ToListAsync();
            ViewBag.Students = new SelectList(students, "StudentCode", "FullName", selectedStudentCode);
        }
    }
}

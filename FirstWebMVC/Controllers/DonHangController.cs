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
                .Include(d => d.KhachHang)
                .OrderBy(d => d.NgayDat)
                .Select(d => new DonHangViewModel
                {
                    DonHangId = d.DonHangId,
                    MaDonHang = d.MaDonHang,
                    NgayDat = d.NgayDat,
                    TongTien = d.TongTien,
                    KhachHangId = d.KhachHangId,
                    TenKhachHang = d.KhachHang != null ? d.KhachHang.TenKhachHang : string.Empty
                })
                .ToListAsync();

            return View(donHangs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.KhachHang)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(d => d.DonHangId == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateKhachHangsAsync();
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

            await PopulateKhachHangsAsync(donHang.KhachHangId);
            return View(donHang);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            await PopulateKhachHangsAsync(donHang.KhachHangId);
            return View(donHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DonHang donHang)
        {
            if (id != donHang.DonHangId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateKhachHangsAsync(donHang.KhachHangId);
                return View(donHang);
            }

            try
            {
                _context.Update(donHang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(donHang.DonHangId))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.DonHangId == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang != null)
            {
                _context.DonHangs.Remove(donHang);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHangs.Any(e => e.DonHangId == id);
        }

        private async Task PopulateKhachHangsAsync(int? selectedKhachHangId = null)
        {
            var khachHangs = await _context.KhachHangs.OrderBy(k => k.TenKhachHang).ToListAsync();
            ViewBag.KhachHangs = new SelectList(khachHangs, "KhachHangId", "TenKhachHang", selectedKhachHangId);
        }
    }
}

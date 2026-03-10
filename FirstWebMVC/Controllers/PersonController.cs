using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Person;

namespace FirstWebMVC.Controllers
{
    public class PersonController : Controller
    {
        // Khai báo biến _context dùng để làm việc với database
        // private = chỉ dùng trong class
        // readonly = chỉ gán giá trị 1 lần
        // ApplicationDbContext = lớp quản lý kết nối database
        private readonly ApplicationDbContext _context;
        
        // Constructor (hàm khởi tạo của Controller)
        // ASP.NET Core sẽ đưa ApplicationDbContext vào đây
        public PersonController(ApplicationDbContext context)
        {
            // gán tham số context cho biến _context để Controller sử dụng database
            _context = context;
        }
        
        // Method Index dùng để hiển thị danh sách Person
        // async = cho phép chạy bất đồng bộ
        // Task<IActionResult> = kiểu trả về của method async trong MVC
        public async Task<IActionResult> Index()
        {
            // lấy toàn bộ dữ liệu từ bảng Person trong database
            // ToListAsync() = lấy dữ liệu bất đồng bộ
            // model = List<Person>
            var model = await _context.Person.ToListAsync();
            
            // trả dữ liệu model sang View để hiển thị
            return View(model);
        }
        
        // Method Create dùng để hiển thị trang thêm dữ liệu mới
        public IActionResult Create()
        {
            // trả về View Create.cshtml để hiển thị form thêm Person
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonID,FullName,Address")] Person person)
        {
            if(id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contet.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(person);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person' is null.");
            }

            var person = await _context.Person.FindAsync(id);
            if(person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PersonExists(string id)
        {
            return(_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefualt();
        }
    }
}
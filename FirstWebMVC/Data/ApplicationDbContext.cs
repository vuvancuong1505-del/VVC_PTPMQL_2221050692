using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Models.Person;
using FirstWebMVC.Models.Student;
using FirstWebMVC.Models.Employee.Employee;
using FirstWebMVC.Models.Family;
using FirstWebMVC.Models.DonHang;

namespace FirstWebMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<Person> Persons {get; set;}

        public DbSet<Student> Students { get; set; }
        public DbSet<Family> Families {get; set; }
        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
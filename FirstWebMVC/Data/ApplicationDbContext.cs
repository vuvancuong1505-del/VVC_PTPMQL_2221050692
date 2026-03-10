using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Models.Person;
using FirstWebMVC.Models.Student;

namespace FirstWebMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<Person> Person {get; set;}

        public DbSet<Student> Students { get; set; }
    }
}
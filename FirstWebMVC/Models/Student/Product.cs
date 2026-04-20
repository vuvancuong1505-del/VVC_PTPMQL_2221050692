using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Model.Student
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; } = default!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khoa chỉ được tối đa {1} ký tự")]
        [Display(Name = "Khoa")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

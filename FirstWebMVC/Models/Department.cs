using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models
{
    [Table("Departments")]
    public class Faculty
    {
        [Key]
        [Column("DepartmentId")]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khoa chỉ được tối đa {1} ký tự")]
        [Display(Name = "Khoa")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

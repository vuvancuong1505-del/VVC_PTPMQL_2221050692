using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [Display(Name = "Mã sinh viên")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Mã sinh viên phải từ {2} đến {1} ký tự")]
        [Display(Name = "Mã sinh viên")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Họ tên phải từ {2} đến {1} ký tự")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email chỉ được tối đa {1} ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Range(16, 100, ErrorMessage = "Tuổi phải nằm trong khoảng {1} đến {2}")]
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
    }
}

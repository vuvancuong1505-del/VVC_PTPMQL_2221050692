using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng chỉ được tối đa {1} ký tự")]
        [Display(Name = "Khách hàng")]
        public string TenKhachHang { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email chỉ được tối đa {1} ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        public ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
    }
}

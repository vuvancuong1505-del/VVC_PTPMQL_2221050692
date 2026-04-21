using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        public string? TenKhachHang { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? DienThoai { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(200)]
        public string? DiaChi { get; set; }

        public ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
    }
}
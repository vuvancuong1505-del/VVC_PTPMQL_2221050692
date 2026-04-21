using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class ChiTietDonHang
    {
        
        [Key]
        public int ChiTietDonHangId { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Đơn giá không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải > 0")]
        public decimal DonGia { get; set; }

        public int DonHangId { get; set; }
        public DonHang? DonHang { get; set; }

        public int SanPhamId { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
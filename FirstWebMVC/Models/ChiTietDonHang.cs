using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangId { get; set; }

        [Required(ErrorMessage = "Đơn hàng không được để trống")]
        [Display(Name = "Đơn hàng")]
        public int DonHangId { get; set; }

        [ForeignKey(nameof(DonHangId))]
        public DonHang? DonHang { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        [Display(Name = "Sản phẩm")]
        public int SanPhamId { get; set; }

        [ForeignKey(nameof(SanPhamId))]
        public SanPham? SanPham { get; set; }

        [Range(1, 1000, ErrorMessage = "Số lượng phải nằm trong khoảng {1} đến {2}")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Range(0.01, 10000000, ErrorMessage = "Đơn giá phải lớn hơn {1}")]
        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }

        [NotMapped]
        [Display(Name = "Thành tiền")]
        public decimal ThanhTien => SoLuong * DonGia;
    }
}

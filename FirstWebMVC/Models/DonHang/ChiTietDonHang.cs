using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class ChiTietDonHang
    {
        
        [Key]
        public int ChiTietDonHangId { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        // FK Đơn hàng
        public int DonHangId { get; set; }

        [ForeignKey("DonHangId")]
        public DonHang DonHang { get; set; }

        // FK Sản phẩm
        public int SanPhamId { get; set; }

        [ForeignKey("SanPhamId")]
        public SanPham SanPham { get; set; }
    }
}
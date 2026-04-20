using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class SanPham
    {
         [Key]
        public int SanPhamId { get; set; }

        public string TenSanPham { get; set; }

        public decimal Gia { get; set; }

        // 1 sản phẩm có nhiều chi tiết đơn hàng
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
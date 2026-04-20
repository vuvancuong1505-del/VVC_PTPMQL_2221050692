using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; }

        public DateTime NgayDat { get; set; }

        // Khóa ngoại
        public int KhachHangId { get; set; }

        [ForeignKey("KhachHangId")]
        public KhachHang KhachHang { get; set; }

        // 1 đơn hàng có nhiều chi tiết
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}   
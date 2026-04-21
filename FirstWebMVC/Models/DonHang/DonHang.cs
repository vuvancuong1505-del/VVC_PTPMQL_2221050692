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

        [Required(ErrorMessage = "Ngày đặt không được để trống")]
        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Phải chọn khách hàng")]
        public int KhachHangId { get; set; }

        public KhachHang? KhachHang { get; set; }

        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}   
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.DonHang
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }

        public string TenKhachHang { get; set; }

        public string DienThoai { get; set; }

        public string DiaChi { get; set; }

        // 1 khách hàng có nhiều đơn hàng
        public ICollection<DonHang> DonHangs { get; set; }
    }
}
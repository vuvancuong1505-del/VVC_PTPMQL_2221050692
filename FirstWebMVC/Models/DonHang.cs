using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        [StringLength(50, ErrorMessage = "Mã đơn hàng chỉ được tối đa {1} ký tự")]
        [Display(Name = "Mã đơn hàng")]
        public string MaDonHang { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày đặt không được để trống")]
        [Display(Name = "Ngày đặt")]
        public DateTime NgayDat { get; set; }

        [Range(0.01, 10000000, ErrorMessage = "Tổng tiền phải lớn hơn {1}")]
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }

        [Required(ErrorMessage = "Sinh viên không được để trống")]
        [Display(Name = "Sinh viên")]
        public string StudentCode { get; set; } = string.Empty;

        [ForeignKey(nameof(StudentCode))]
        public Student? Student { get; set; }
    }
}

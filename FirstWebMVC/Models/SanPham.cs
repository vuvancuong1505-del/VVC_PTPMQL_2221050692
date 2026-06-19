using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm chỉ được tối đa {1} ký tự")]
        [Display(Name = "Sản phẩm")]
        public string TenSanPham { get; set; } = string.Empty;

        [Range(0.01, 10000000, ErrorMessage = "Giá phải lớn hơn {1}")]
        [Display(Name = "Giá bán")]
        public decimal Gia { get; set; }

        [StringLength(250, ErrorMessage = "Mô tả chỉ được tối đa {1} ký tự")]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}

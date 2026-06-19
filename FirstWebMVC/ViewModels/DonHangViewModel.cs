namespace FirstWebMVC.ViewModels
{
    public class DonHangViewModel
    {
        public int DonHangId { get; set; }
        public string MaDonHang { get; set; } = string.Empty;
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public int KhachHangId { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
    }
}

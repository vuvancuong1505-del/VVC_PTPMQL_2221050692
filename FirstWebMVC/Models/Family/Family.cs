using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Family
{
    [Table("Families")]
    public class Family
    {
        [Key]
        public int ID{get; set; }
        [Required(ErrorMessage="Vui lòng nhập tên của bạn")]
        [StringLength(30, ErrorMessage="Tên tối đa 30 ký tự")]
        public string Name{get; set; }
        [Required(ErrorMessage="Vui lòng nhập tuổi của bạn")]
        [Range(1,100, ErrorMessage="Tuổi giới hạn từ 1-100")]
        public int Age{get; set; }
        [StringLength(30, MinimumLength=2, ErrorMessage="Vai trò từ 2-30 kí tự")]
        public string Role{get; set; }
        [Display(Name="Ảnh")]
        [Url(ErrorMessage = "Link ảnh không hợp lệ")]
        public string ImageUrl{get; set; }
    }
}




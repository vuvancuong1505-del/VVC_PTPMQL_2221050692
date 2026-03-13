using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Student
{
    [Table("Students")]
    public class Student
    {   
        [Key]
        public string StudentCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        
        
        // [Key]
        // public int StudentID {get; set;}
        // [Required]
        // public string StudentCode {get; set; }  // Student: là thuộc tính
        // public string FullName {get; set; }     // FullName: là thuộc tính
    }
}



using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}

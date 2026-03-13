using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Person
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public string PersonID{get; set;}
        [Required]
        public string FullName{get; set;}
        public string Address{get; set;}
    }
}
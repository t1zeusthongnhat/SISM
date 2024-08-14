using System.ComponentModel.DataAnnotations;

namespace StudentManager.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; } 

    }
}

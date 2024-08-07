using System.ComponentModel.DataAnnotations;

namespace StudentManager.Models
{
    public class Course
    {
        public int Id { get; set; }
   
        [Required(ErrorMessage = "Full Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

     
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string institute { get; set; }
        [Required(ErrorMessage = "Please enter a Degree Level")]
        public string degreeLevel { get; set; }

        [Required(ErrorMessage = "Please enter a Degree Name")]
        public string degreeName { get; set; }
        [Required(ErrorMessage = "Please enter your grade")]
        public string grade { get; set; }
        [Required]
        public string passingYear { get; set; }
        public string achievement { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public User user { get; set; }
        public int UserID { get; set; }
    }
}

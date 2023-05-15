using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class EducationDto
    {
        [Required]
        public string institute { get; set; }
        [Required(ErrorMessage = "Please enter a Degree Level")]
        public string degreeLevel { get; set; }
        public string degreeName { get; set; }
        [Required(ErrorMessage = "Please enter your grade")]
        public string grade { get; set; }
        [Required]
        public string passingYear { get; set; }
        public string achievement { get; set; }
        
    }
}

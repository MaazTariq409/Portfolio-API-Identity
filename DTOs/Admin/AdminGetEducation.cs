using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs.Admin
{
    public class AdminGetEducation
    {
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
        public string status { get; set; } = "pending";
    }
}

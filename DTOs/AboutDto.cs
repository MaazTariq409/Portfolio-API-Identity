using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class AboutDto
    {
        [Required(ErrorMessage = "Please upload an image")]
        public string ProfileUrl { get; set; }
        public string Introduction { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter First Name")]
        public string Name { get; set; }
        [Required]
        public string Dob { get; set; }
        [Required(ErrorMessage = "Please enter an Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Linkedin { get; set; }

        public string Github { get; set; }
        [Required(ErrorMessage = "Please enter a Phone No")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        [Required]
        public string Gender { get; set; }
        
        
        
        
        
    }
}

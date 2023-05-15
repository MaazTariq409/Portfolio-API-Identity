using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class UserProjectsDto
    {
        [Required(ErrorMessage = "Please enter a Project Name")]
        public string ProjectTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Stack { get; set; }
        [Required]
        public string GitUrl { get; set; }
    }
}

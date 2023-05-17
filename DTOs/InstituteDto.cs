using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class InstituteDto
    {
        [Required(ErrorMessage = "Enter the Institute")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the BranchNo")]
        public string BranchNo { get; set; }
        [Required(ErrorMessage = "Enter the Website Url")]
        public string Website { get; set; }
    }
}

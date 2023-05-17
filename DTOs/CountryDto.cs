using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class CountryDto
    {
        [Required(ErrorMessage = "Enter the Country")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter the Alpha3")]
        public string Alpha3 { get; set; }
    }
}

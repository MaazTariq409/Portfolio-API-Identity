using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class CityDto
    {
        [Required(ErrorMessage = "Enter the City")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the Region")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Enter the Zip Code")]
        public string ZipCode { get; set; }
    }
}

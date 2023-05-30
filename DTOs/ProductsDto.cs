using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio_API.DTOs
{
    public class ProductsDto
    {
        [Required(ErrorMessage = "Please enter a Product Name")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PermaLink { get; set; }
        [Required]
        public string DateCreated { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string VideoUrl { get; set; }

    }
}

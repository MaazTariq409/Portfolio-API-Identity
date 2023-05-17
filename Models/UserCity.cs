using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserCity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the City")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the Region")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Enter the Zip Code")]
        public string ZipCode { get; set; }
    }
}

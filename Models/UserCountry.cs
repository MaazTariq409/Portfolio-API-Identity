using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserCountry
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the Country")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter the Alpha3")]
        public string Alpha3 { get; set; }
    }
}

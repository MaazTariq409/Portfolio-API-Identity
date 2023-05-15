using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserExperience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string companyName { get; set; }
        [Required]
        public string jobTitle { get; set; }
        public string responsibility { get; set; }
        public string duration { get; set; }

        [ForeignKey("userID")]
        [ValidateNever]
        public User user { get; set; }
        public int userID { get; set; }
    }
}

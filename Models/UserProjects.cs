using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class UserProjects
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Project Name")]
        public string ProjectTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Stack { get; set; }
        [Required]
        public string GitUrl { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public User user { get; set; }
        public int UserID { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Skill")]
        public string SkillName { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public User user { get; set; }
        public int UserID { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs.Admin
{
    public class AdminGetSkillDto
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Skill")]
        public string SkillName { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        public string status { get; set; } = "pending";
    }
}   

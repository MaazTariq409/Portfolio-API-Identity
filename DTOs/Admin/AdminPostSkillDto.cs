using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs.Admin
{
    public class AdminPostSkillDto
    {
        public string SkillName { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        public string status { get; set; } = "pending";
    }
}

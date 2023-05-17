using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class adminSkillDto
    {
        public string SkillName { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        public string status { get; set; } = "pending";
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter an Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string password { get; set; }

        public About About { get; set; }
        [ValidateNever]
        public List<Education> Education { get; set; }
        [ValidateNever]
        public List<Skills> Skills { get; set; }
        [ValidateNever]
        public List<UserProjects> UserProjects { get; set; }
        public List<UserExperience> UserExperiences { get; set; }

    }
}

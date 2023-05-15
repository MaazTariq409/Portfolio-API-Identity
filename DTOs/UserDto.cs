using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "Please enter a Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter an Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string password { get; set; }
     
	}
}

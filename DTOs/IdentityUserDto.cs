using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
	public class IdentityUserDto
	{
		[Required(ErrorMessage = "Please enter a Username")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Please enter an Email")]
		public string Email { get; set; }

		[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
		public string Password { get; set; }

	}
}

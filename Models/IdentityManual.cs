using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models
{
	public class IdentityManual : IdentityUser
	{
		//[Required(ErrorMessage = "Please enter a Username")]
		//public string UserName { get; set; }
		//[Required(ErrorMessage = "Please enter an Email")]
		//public string Email { get; set; }

		//[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
		//public string Password { get; set; }
	}
}

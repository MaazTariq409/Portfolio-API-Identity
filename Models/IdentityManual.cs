using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models
{
	public class IdentityManual : IdentityUser
	{
		public UserProfile UserProfile { get; set; }
	}
	public class IdentityRoles : IdentityRole
	{

	}
}

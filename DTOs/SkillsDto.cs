using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Portfolio_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_API.DTOs
{
	public class SkillsDto
	{
		[Required(ErrorMessage = "Please enter a Skill")]
		public string SkillName { get; set; }
		[Required]
		public string SkillLevel { get; set; }

	}
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using System.Security.Principal;

namespace Portfolio_API.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{

		private readonly IMapper _mapper;

		private readonly UserManager<IdentityManual> _userManager;
		private readonly SignInManager<IdentityManual> _signInManager;
		//private readonly RoleManager<IdentityManual> _roleManager;

		public AccountController(
			UserManager<IdentityManual> userManager,
			SignInManager<IdentityManual> signInManager,
			//RoleManager<IdentityManual> roleManager,
			IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			//_roleManager = roleManager;
			_mapper = mapper;

		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(IdentityManual model)
		{
			if (model == null)
			{
				return NotFound();
			}
			else
			{
				var identityUser = new IdentityManual()
				{
					Email = model.NormalizedEmail,
					UserName= model.NormalizedUserName,
				};
				// var user = new IdentityUserDto { UserName = model.UserName, Email = model.Email };
				//var AboutDto = _mapper.Map<IdentityUserDto>(user);

				var result = await _userManager.CreateAsync(identityUser, model.PasswordHash);
				
				if (result.Succeeded)
				{
					return Ok();
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(IdentityManual model)
		{
			if (model == null)
			{
				return NotFound(model);
			}
			else
			{
				var result = await _signInManager.PasswordSignInAsync(model.NormalizedEmail, model.PasswordHash, false, false);
				if (result.Succeeded)
				{
					return Ok();
				}
				else
				{
					return NotFound();
				}
			}
		}


	}
}

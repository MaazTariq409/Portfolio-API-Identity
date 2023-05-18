using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Portfolio_API.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{

		private readonly IMapper _mapper;

		private readonly UserManager<IdentityManual> _userManager;
		private readonly SignInManager<IdentityManual> _signInManager;
		private readonly IConfiguration _configuration;

		//private readonly RoleManager<IdentityManual> _roleManager;

		public AccountController(
			UserManager<IdentityManual> userManager,
			SignInManager<IdentityManual> signInManager,
			//RoleManager<IdentityManual> roleManager,
			IConfiguration configuration,
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
		public async Task<IActionResult> Login(IdentityUserDto model)
		{
			var user = await _userManager.FindByNameAsync(model.Email);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				return Ok();
			}
			return NotFound();

			//if (model == null)
			//{
			//	return NotFound(model);
			//}
			//else
			//{
			//	var idenityuser = new IdentityManual();
			//	idenityuser.Email = model.Email;
			//	idenityuser.PasswordHash = model.Password;

			//	var result = await _signInManager.PasswordSignInAsync(idenityuser.Email, idenityuser.PasswordHash, false, lockoutOnFailure: false);

			//	if (result.Succeeded)
			//	{
			//		return Ok();
			//	}
			//	else
			//	{
			//		return NotFound();
			//	}
			//}
		}


	}
}

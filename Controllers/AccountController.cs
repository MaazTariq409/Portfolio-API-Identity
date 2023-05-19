using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Utility;


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
		private readonly TokenGeneration _token;


		//private readonly RoleManager<IdentityRoles> _roleManager;

		public AccountController(
			UserManager<IdentityManual> userManager,
			SignInManager<IdentityManual> signInManager,
			//RoleManager<IdentityRoles> roleManager,
			IConfiguration configuration,
			IMapper mapper,
			TokenGeneration token
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			//_roleManager = roleManager;
			_mapper = mapper;
			_token = token;

		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(IdentityUserDto model)
		{
			//if (!_roleManager.RoleExistsAsync(UserRoles.Role_Admin).GetAwaiter().GetResult())
			//{
			//	_roleManager.CreateAsync(new IdentityRoles(UserRoles.Role_Admin)).GetAwaiter().GetResult();
			//	_roleManager.CreateAsync(new IdentityRoles(UserRoles.Role_User_Indi)).GetAwaiter().GetResult();

			//}

			var userExists = await _userManager.FindByNameAsync(model.UserName);
		
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError);

			var identityUser = new IdentityManual()
			{
				UserName = model.UserName,
				Email = model.Email,
				PasswordHash = model.Password
			};
			var result = await _userManager.CreateAsync(identityUser, model.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(IdentityUserDto model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var Token = _token.TokenGenerator(user);

				var tokenToReturn = new Tokenmodel();
				tokenToReturn.Token = Token;

				return Ok(tokenToReturn);
			}
			return NotFound();

		}


	}
}

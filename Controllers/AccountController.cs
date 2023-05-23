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
		private ResponseObject _responseObject;


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

			var userExists = await _userManager.FindByNameAsync(model.UserName);
		
			if (userExists != null)
			{
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User already exists");

				return BadRequest(_responseObject);
			}

				var identityUser = new IdentityManual()
				{
					UserName = model.UserName,
					Email = model.Email,
					PasswordHash = model.Password
				};

				var result = await _userManager.CreateAsync(identityUser, model.Password);
				if (!result.Succeeded)
					return StatusCode(StatusCodes.Status500InternalServerError);		
			var Token = _token.TokenGenerator(identityUser);

			var tokenToReturn = new Tokenmodel();
			tokenToReturn.Token = Token;
			_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Successfull", tokenToReturn);
			return Ok(_responseObject);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				var Token = _token.TokenGenerator(user);

				var tokenToReturn = new Tokenmodel();
				tokenToReturn.Token = Token;
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Successfull", tokenToReturn);
				return Ok(_responseObject);
			}
			else
			{
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
				return NotFound(_responseObject);

			}
			
		}


	}
}

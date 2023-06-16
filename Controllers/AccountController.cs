using AutoMapper;
using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountController));


        private readonly IMapper _mapper;
		private readonly ILogger<AccountController> _logger;
		//private readonly ILog _netLogger;
		private readonly UserManager<IdentityManual> _userManager;
		private readonly SignInManager<IdentityManual> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly TokenGeneration _token;
		private ResponseObject _responseObject;


		//private readonly RoleManager<IdentityRoles> _roleManager;

		public AccountController(
			UserManager<IdentityManual> userManager,
			SignInManager<IdentityManual> signInManager,
			IConfiguration configuration,
			IMapper mapper,
			TokenGeneration token,
			ILogger<AccountController> logger
            //ILog netLogger
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_token = token;
			_logger = logger;
            //_netLogger = netLogger;


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
					Role = model.Role,
					PasswordHash = model.Password
				};

				var result = await _userManager.CreateAsync(identityUser, model.Password);
				if (!result.Succeeded)
					return StatusCode(StatusCodes.Status500InternalServerError);		
			var Token = _token.TokenGenerator(identityUser);

			var tokenToReturn = new Tokenmodel();
			tokenToReturn.Token = Token;
			_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Successfull", tokenToReturn, model.Role );
			return Ok(_responseObject);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto model)
		{
            //_netLogger.Info("This is an information Net4Log message.");
            //_netLogger.Error("This is an error Net4Log message.");

            log.Debug("This is a debug message.");
            log.Info("This is an info message.");
            log.Warn("This is a warning message.");
            log.Error("This is an error message.");
            log.Fatal("This is a fatal message.");

            var user = await _userManager.FindByEmailAsync(model.Email);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{

				_logger.LogError("User Login Started");

                


                //_roleManager.CreateAsync(new IdentityRole(Role_User));
                var userRoles = await _userManager.GetRolesAsync(user);
				var Token = _token.TokenGenerator(user);

				var tokenToReturn = new Tokenmodel();
				tokenToReturn.Token = Token;
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Successfull", tokenToReturn, user.Role);

                _logger.LogInformation("User LogedIn");

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

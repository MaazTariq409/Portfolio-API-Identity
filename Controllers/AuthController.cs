using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Data;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PorfolioContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenGeneration _token;
		private ResponseObject _responseObject;

		public AuthController(PorfolioContext context, IConfiguration configuration, TokenGeneration token)
        {
            _context = context;
            _configuration = configuration;
            _token = token;
        }

        [HttpPost]
        public ActionResult<Tokenmodel> userAuthentication(SignIn signIn)
        {
            var user = _token.validateUserInput(signIn.Email, signIn.Password);

            if (user == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User Unauthorized");
                return Unauthorized(_responseObject);
            }

            var tokenToReturn = _token.TokenGenerator(user);
            var token = new Tokenmodel();
            token.Token = tokenToReturn;

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Login Succesfull", token);
            return Ok(_responseObject);
        }
    }
}

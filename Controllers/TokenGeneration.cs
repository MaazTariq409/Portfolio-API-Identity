using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Portfolio_API.Data;
using Portfolio_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolio_API.Controllers
{
    public class TokenGeneration
    {
        private readonly PorfolioContext _context;
        private readonly IConfiguration _configuration;
		private readonly UserManager<IdentityManual> _userManager;


		public TokenGeneration(PorfolioContext context,
            UserManager<IdentityManual> userManager,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
			_userManager = userManager;

		}
		public string TokenGenerator(IdentityManual user)
        {

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var TokenClaims = new List<Claim>();
            TokenClaims.Add(new Claim("UserId", user.Id.ToString()));
            TokenClaims.Add(new Claim("UserName", user.UserName.ToString()));
            TokenClaims.Add(new Claim("Email", user.Email.ToString()));
            TokenClaims.Add(new Claim("Role", user.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["authentication:Audiance"],
                TokenClaims,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        public IdentityManual validateUserInput(string email, string password)
        {
			var validUser = _context.identityManuals.FirstOrDefault(x => x.Email == email);

			//var validUser = _context.user.FirstOrDefault(x => x.email == email);
			if (validUser != null)
            {
                if (validUser.PasswordHash == password)
                {
                    return validUser;
                }
            }
                return null;
        }
    }
}

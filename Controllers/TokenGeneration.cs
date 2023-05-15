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

        public TokenGeneration(PorfolioContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string TokenGenerator(User user)
        {

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var TokenClaims = new List<Claim>();
            TokenClaims.Add(new Claim("UserId", user.Id.ToString()));
            TokenClaims.Add(new Claim("UserName", user.username.ToString()));
            TokenClaims.Add(new Claim("Email", user.email.ToString()));

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

        public User validateUserInput(string email, string password)
        {
            var validUser = _context.user.FirstOrDefault(x => x.email == email);
            if (validUser != null)
            {
                if (validUser.password == password)
                {
                    return validUser;
                }
            }
                return null;
        }
    }
}

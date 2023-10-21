using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulseAPI.Repositories
{
    public class TokenRepoImplement : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepoImplement(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwnToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            claim.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role)));

            // JWT Security Token generation
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claim,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(15));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

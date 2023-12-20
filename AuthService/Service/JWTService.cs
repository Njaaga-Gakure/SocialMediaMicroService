using AuthService.Models;
using AuthService.Service.IService;
using AuthService.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Service
{
    public class JWTService : IJWT
    {
        private readonly JwtOptions _jwtOptions;

        public JWTService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }
        public string CreateToken(User user, IEnumerable<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"));

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using RegistroUsuario.Dto;
using RegistroUsuario.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistroUsuario.Services
{
    public class TokenGenerator
    {
        public string generateToken(UserDto user)
        {
            string host = Environment.GetEnvironmentVariable("HOST");
            string secretClient = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretClient));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                //new Claim("id",user.id.ToString()),
                new Claim(ClaimTypes.Email,user.email),
                new Claim(ClaimTypes.Name,user.name),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                host,
                host,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

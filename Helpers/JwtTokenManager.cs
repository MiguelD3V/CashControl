using Cashcontrol.API.Helpers.Interface;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cashcontrol.API.Helpers
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(LoginRequestDto userDto)
        {
            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Name, userDto.Name)
            };

            var jwtKey = _configuration["JwtSettings:SecurityKey"]
                         ?? throw new InvalidOperationException("Chave JWT não configurada.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDuration = _configuration.GetValue<int>("JwtSettings:TokenDurationInMinutes");

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenDuration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using Cashcontrol.API.Helpers.Interface;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Name, userDto.Name)
            };

            var jwtKey = _configuration.GetValue<string>("JwtSettings:SecurityKey");
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey ?? string.Empty));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDurationStr = _configuration.GetSection("JwtSettings:TokenDurationInMinutes").Value;
            var tokenExpiringTime = double.TryParse(tokenDurationStr, out var duration) ? duration : 60; // valor padrão 60 minutos

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenExpiringTime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

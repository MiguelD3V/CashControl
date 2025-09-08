using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Helpers.Interface
{
    public interface IJwtTokenManager
    {
        public string GenerateToken(LoginRequestDto userDto);
    }
}

using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseDto> RegisterAsync(RegistrerRequestDto user);
        public Task<AuthResponseDto> LoginAsync(LoginRequestDto user);

    }
}

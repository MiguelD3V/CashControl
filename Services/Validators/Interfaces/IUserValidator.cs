using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IUserValidator
    {
        public Task<UserResponseDto> ValidateToRegister(RegistrerRequestDto user);
        public Task<UserResponseDto> ValidateToLogin(LoginRequestDto user);
    }
}

using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IUserValidator
    {
        public UserResponseDto ValidateToRegister(RegistrerRequestDto user);
        public UserResponseDto ValidateToLogin(LoginRequestDto user);
    }
}

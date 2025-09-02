using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;

namespace Cashcontrol.API.Services.Workers
{
    public class UserService : IUserService
    {
        private readonly IUserValidator _userValidator;
        public UserService(IUserValidator userValidator)
        {
            _userValidator = userValidator;
        }
        public Task<AuthResponseDto> LoginAsync(LoginRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto> RegisterAsync(RegistrerRequestDto user)
        {
           var ValidationResult = _userValidator.ValidateToRegister(user);
           if(!ValidationResult.Success)
            {
                return Task.FromResult(ValidationResult);
            }


        }
    }
}

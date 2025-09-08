using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository repository)
        {
            _userRepository = repository;

        }

        public async Task<UserResponseDto> ValidateToLogin(LoginRequestDto user)
        {
            var response = new UserResponseDto();
            var findUser = await _userRepository.GetUserByEmailAsync(user.Email);

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                response.Success = false;
                response.Message = "Validation failed.";
                response.Errors.Add("Email and Password are required.");
            }
            if(findUser == null)
            {
                response.Success = false;
                response.Message = "Validation failed.";
                response.Errors.Add("User not found.");
            }
       
            return response;
        }

        public async Task<UserResponseDto> ValidateToRegister(RegistrerRequestDto user)
        {
            var response = new UserResponseDto();
            var findUser = await _userRepository.GetUserByEmailAsync(user.Email);

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Name))
            {
                response.Success = false;
                response.Message = "Validation failed.";
                response.Errors.Add("Email, Password and Name are required.");
            }
            if (findUser != null)
            {
                response.Success = false;
                response.Message = "Validation failed.";
                response.Errors.Add("Email already in use.");
            }
            return response;
        }
    }
}

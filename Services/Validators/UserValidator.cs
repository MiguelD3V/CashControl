using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly UserResponseDto _response;
        private readonly IUserRepository _userRepository;

        public UserValidator(UserResponseDto response, IUserRepository repository)
        {
            _response = response;
            _userRepository = repository;

        }

        public UserResponseDto ValidateToLogin(LoginRequestDto user)
        {
            var findUser = _userRepository.GetUserByEmailAsync(user.Email);

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                _response.Success = false;
                _response.Message = "Validation failed.";
                _response.Errors.Add("Email and Password are required.");
            }
            if(findUser == null)
            {
                _response.Success = false;
                _response.Message = "Validation failed.";
                _response.Errors.Add("User not found.");
            }
            return _response;
        }

        public UserResponseDto ValidateToRegister(RegistrerRequestDto user)
        {
            var findUser = _userRepository.GetUserByEmailAsync(user.Email);
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Name))
            {
                _response.Success = false;
                _response.Message = "Validation failed.";
                _response.Errors.Add("Email, Password and Name are required.");
            }
            if (findUser != null)
            {
                _response.Success = false;
                _response.Message = "Validation failed.";
                _response.Errors.Add("Email already in use.");
            }
            return _response;
        }
    }
}

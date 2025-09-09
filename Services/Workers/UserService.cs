using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Helpers.Interface;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Cashcontrol.API.Services.Workers
{
    public class UserService : IUserService
    {
        private readonly IUserValidator _userValidator;
        private readonly IPasswordHelper _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenManager _jwtTokenManager;
        public UserService(IUserValidator userValidator, IPasswordHelper passwordHelper, IUserRepository userRepository, IJwtTokenManager jwtTokenManager)
        {
            _userValidator = userValidator;
            _passwordHasher = passwordHelper;
            _userRepository = userRepository;
            _jwtTokenManager = jwtTokenManager;
        }


        public async Task<UserResponseDto> RegisterAsync(RegistrerRequestDto user)
        {
            var validationResult = await _userValidator.ValidateToRegister(user);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            var passwordEncryption = _passwordHasher.CreatePasswordHash(user.Password);

            var userModel = new User
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = Convert.ToBase64String(passwordEncryption.PasswordHash),
                PasswordSalt = Convert.ToBase64String(passwordEncryption.PasswordSalt),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateUserAsync(userModel);

            return new UserResponseDto
            {
                Success = true,
                Message = "Usuário registrado com sucesso.",
                Id = userModel.Id.GetHashCode(),
                Username = userModel.Name,
                Email = userModel.Email,
                Role = "User"
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto user)
        {
            var validationResult = await _userValidator.ValidateToLogin(user);

            if (!validationResult.Success)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Falha na autenticação.",
                    Errors = validationResult.Errors
                };
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Errors = new List<string> { "Usuário não encontrado." }
                };
            }

            var isPasswordValid = _passwordHasher.VerifyPasswordHash(
                user.Password,
                Convert.FromBase64String(existingUser.PasswordHash),
                Convert.FromBase64String(existingUser.PasswordSalt)
            );

            if (!isPasswordValid)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Senha incorreta.",
                    Errors = new List<string> { "Senha incorreta." }
                };
            }
            var token = _jwtTokenManager.GenerateToken(new LoginRequestDto
            {
                Email = existingUser.Email,
                Name = existingUser.Name,
            });

            // Sucesso na autenticação
            return new AuthResponseDto
            {
                Success = true,
                Message = "Login realizado com sucesso.",
                Token = token
            };
        }
    }
}

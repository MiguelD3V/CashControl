using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class UserValidator : IUserValidator
    {
        public ResponseBase ValidateToLogin(LoginRequestDto user)
        {
            throw new NotImplementedException();
        }

        public ResponseBase ValidateToRegister(RegistrerRequestDto user)
        {
            throw new NotImplementedException();
        }
    }
}

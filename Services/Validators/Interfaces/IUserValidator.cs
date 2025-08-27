using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IUserValidator
    {
        public ResponseBase ValidateToRegister(RegistrerRequestDto user);
        public ResponseBase ValidateToLogin(LoginRequestDto user);
    }
}

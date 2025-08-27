using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Workers.Interfaces;

namespace Cashcontrol.API.Services.Workers
{
    public class UserService : IUserService
    {
        public Task<AuthResponseDto> RegisterAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Task<AuthResponseDto> LoginAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

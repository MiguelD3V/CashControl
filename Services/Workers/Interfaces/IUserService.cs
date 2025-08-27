using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IUserService
    {
        public Task<AuthResponseDto> RegisterAsync { get; set; }
        public Task<AuthResponseDto> LoginAsync { get; set; }

    }
}

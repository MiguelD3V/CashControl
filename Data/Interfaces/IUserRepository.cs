using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(User user);
    }
}

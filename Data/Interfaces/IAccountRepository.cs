using Cashcontrol.API.Models.Bussines;
using System.Collections.Immutable;

namespace Cashcontrol.API.Data.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account?> CreateAsync(Account account);
        public Task<Account?> UpdateAsync (Account account);
        public Task DeleteAsync(Account account);
        public Task<ImmutableList<Account>> GetAllAsync();
        public Task<Account> GetByIdAsync(Guid id);
        public Task<Account> GetByNameAsync(string name);
        public Task<Account> GetByEmailAsync(string email);
    }
}

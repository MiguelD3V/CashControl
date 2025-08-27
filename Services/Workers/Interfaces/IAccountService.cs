using Cashcontrol.API.Models.Dtos.Account;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountResponseDto> CreateAsync(AccountRequestDto account);
        public Task<AccountResponseDto> UpdateAsync(string emial, AccountUpdateRequestDto account);
        public Task<AccountResponseDto> GetByIdAsync(Guid id);
        public Task<IImmutableList<AccountResponseDto>> GetAllAsync();
        public Task<AccountResponseDto> GetByEmailAsync(string email);
        public Task<AccountResponseDto> DeleteAsync(Guid id);
    }
}

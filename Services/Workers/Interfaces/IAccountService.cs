using Cashcontrol.API.Models.Dtos;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountResponseDto> CreateAsync(AccountRequestDto account);
        public Task<AccountResponseDto> UpdateAsync(AccountRequestDto account);
        public Task<AccountResponseDto> GetByIdAsync(Guid id);
        public Task<IImmutableList<AccountResponseDto>> GetAllAsync();
        public Task<AccountResponseDto> DeleteAsync(Guid id);
    }
}

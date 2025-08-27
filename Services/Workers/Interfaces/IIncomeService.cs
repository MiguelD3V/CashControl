using Cashcontrol.API.Models.Dtos.Income;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IIncomeService
    {
        public Task<IncomeResponseDto> CreateAsync(IncomeRequestDto income);
        public Task<IncomeResponseDto> UpdateAsync(IncomeRequestDto income, Guid id);
        public Task<IncomeResponseDto> DeleteAsync(Guid id);
        public Task<IncomeResponseDto> GetByIdAsync(Guid id);
        public Task<IImmutableList<IncomeResponseDto>> GetAllAsync();

    }
}

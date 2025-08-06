using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using System.Collections.Immutable;

namespace Cashcontrol.API.Data.Interfaces
{
    public interface IIncomeRepository
    {
        Task CreateAsync(Income income);
        Task UpdateAsync(Income income);
        Task DeleteAsync(Income income);
        Task<ImmutableList<Income>> GetAllAsync();
        Task<Income> GetByIdAsync(Guid id);
    }
}

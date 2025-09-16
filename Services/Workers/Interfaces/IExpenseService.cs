using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IExpenseService
    {
        public Task<ImmutableList<ExpenseResponseDto>> GetAllExpensesAsync();
        public Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseRequestDto expense);
        public Task<ExpenseResponseDto> UpdateExpenseAsync(Guid id, ExpenseRequestDto expense);
        public Task<ExpenseResponseDto> DeleteExpenseAsync(Guid id);
        public Task<ExpenseResponseDto> GetExpenseByName(string name);
        public Task<ExpenseResponseDto> GetExpenseById(Guid id);


    }
}

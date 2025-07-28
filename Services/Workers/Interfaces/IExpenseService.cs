using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers.Interfaces
{
    public interface IExpenseService
    {
        public Task<ImmutableList<Expense>> GetAllExpensesAsync();
        public Task<Expense> GetExpensesByCategoryAsync(string category);
        public Task<Expense> GetExpensesByPaymentMethodAsync(string paymentMethod);
        public Task<decimal> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate);
        public Task<Expense> CreateExpenseAsync(ExpenseRequestDto expense);
        public Task<Expense> UpdateExpenseAsync(Guid id, ExpenseRequestDto expense);
        public Task DeleteExpenseAsync(Guid id);
        public Task<Expense> GetExpenseByName(string name);
        public Task<Expense> GetExpenseById(Guid id);


    }
}

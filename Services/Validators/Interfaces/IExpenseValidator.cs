using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IExpenseValidator
    {
        public ExpenseResponseDto ValidateToCreate(Expense expense);
        public ExpenseResponseDto ValidateToUpdate(Expense expense);
        public ExpenseResponseDto ValidateToDelete(Guid id);
        public ExpenseResponseDto ValidateToGet(Guid id);

    }
}

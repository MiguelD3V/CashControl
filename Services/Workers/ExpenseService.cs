using AutoMapper;
using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Banco.Repositories;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Workers.Interfaces;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseRequestDto expense)
        { 
            var ExpenseDto = new Expense
            {
                Name = expense.Name,
                Description = expense.Description,
                Date = DateTime.UtcNow.Date,
                Amount = expense.Amount,
                AccountId = expense.AccountId,
                Category = expense.Category
            };

            var account = await _accountRepository.GetByIdAsync(expense.AccountId);

            account.Balance -= expense.Amount;

            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "Expense cannot be null");
            }

            await _expenseRepository.Create(ExpenseDto);
            
            return new ExpenseResponseDto 
            {
                Success = true,
                Data = ExpenseDto,
            };

        }

        public async Task<ExpenseResponseDto> DeleteExpenseAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var expense = await _expenseRepository.GetById(id);
            if (expense == null)
            {
                throw new Exception($"Expense with ID {id} not found.");
            }
            await _expenseRepository.Delete(id);
            return new ExpenseResponseDto
            {
                Success = true,
                Message = $"Expense with ID {id} deleted successfully."
            };
        }

        public async Task<ImmutableList<ExpenseResponseDto>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAll();
            if (expenses == null || !expenses.Any())
            {
                throw new Exception("No expenses found.");
            }
            expenses.ToImmutableList<Expense>();

            return expenses
                .Select(Expense => new ExpenseResponseDto
                {
                        Name = Expense.Name,
                        Description = Expense.Description,
                        Date = Expense.Date,
                        Amount = Expense.Amount,
                        AccountId = Expense.AccountId,
                        Category = Expense.Category

                }).ToList().ToImmutableList();
        }

        public async  Task<ExpenseResponseDto> GetExpenseById(Guid id)
        {
            var expense = await _expenseRepository.GetById(id);
            if (expense == null)
            {
                throw new Exception($"Expense with ID {id} not found.");
            }
            return new ExpenseResponseDto 
            {
                Success = true,
                Data = expense
            };
        }

        public async Task<ExpenseResponseDto> GetExpenseByName(string name)
        {
            var expense = await _expenseRepository.GetByName(name);
            if (expense == null)
            {
                throw new Exception($"Expense with name {name} not found.");
            }
            return new ExpenseResponseDto
            { 
                Success = true,
                Data = expense
            };
        }

        public Task<ExpenseResponseDto> GetExpensesByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseResponseDto> GetExpensesByPaymentMethodAsync(string paymentMethod)
        {
            throw new NotImplementedException();
        }

        public async Task<ExpenseResponseDto> UpdateExpenseAsync(Guid id, ExpenseRequestDto expense)
        {
            var existingExpense = await _expenseRepository.GetById(id);

            if (existingExpense == null)
            {
                throw new Exception($"Expense with ID {id} not found.");
            }

            var diference = existingExpense.Amount - expense.Amount;

            var account = await _accountRepository.GetByIdAsync(expense.AccountId);

            account.Balance -= diference;

            var mapExpense = new Expense 
            { 
                Name = expense.Name,
                Description = expense.Description,
                Date = DateTime.UtcNow.Date,
                Amount = expense.Amount,
                AccountId = expense.AccountId,
                Category = expense.Category
            };
            mapExpense.Id = id;

            await _expenseRepository.Update(mapExpense);
            await _accountRepository.UpdateAsync(account);

            return new ExpenseResponseDto 
            {
                Success = true,
                Data = mapExpense
            };
        }
    }
}

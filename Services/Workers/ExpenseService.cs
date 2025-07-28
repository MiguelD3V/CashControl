using AutoMapper;
using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Banco.Repositories;
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

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<Expense> CreateExpenseAsync(ExpenseRequestDto expense)
        { 
            var mapExpense = _mapper.Map<Expense>(expense);
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "Expense cannot be null");
            }

            await _expenseRepository.Create(mapExpense);
            
            return mapExpense;

        }

        public async Task DeleteExpenseAsync(Guid id)
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

        }

        public async Task<ImmutableList<Expense>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAll();
            if (expenses == null || !expenses.Any())
            {
                throw new Exception("No expenses found.");
            }
            expenses.ToImmutableList<Expense>();

            return expenses.ToImmutableList();
        }

        public Task<Expense> GetExpenseById(Guid id)
        {
            var expense = _expenseRepository.GetById(id);
            if (expense == null)
            {
                throw new Exception($"Expense with ID {id} not found.");
            }
            return expense;
        }

        public Task<Expense> GetExpenseByName(string name)
        {
            var expense = _expenseRepository.GetByName(name);
            if (expense == null)
            {
                throw new Exception($"Expense with name {name} not found.");
            }
            return expense;
        }

        public Task<Expense> GetExpensesByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> GetExpensesByPaymentMethodAsync(string paymentMethod)
        {
            throw new NotImplementedException();
        }

        public async Task<Expense> UpdateExpenseAsync(Guid id, ExpenseRequestDto expense)
        {
            var existingExpense = _expenseRepository.GetById(id);
            if (existingExpense == null)
            {
                throw new Exception($"Expense with ID {id} not found.");
            }
            var mapExpense = _mapper.Map<Expense>(expense);
            mapExpense.Id = id;

            await _expenseRepository.Update(mapExpense);
            return mapExpense;
        }
    }
}

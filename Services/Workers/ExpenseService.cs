using AutoMapper;
using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Banco.Repositories;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IExpenseValidator _validator;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IAccountRepository accountRepository, IExpenseValidator validator)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _validator = validator;
        }

        public async Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseRequestDto expense)
        { 
            var expenseModel = _mapper.Map<Expense>(expense);
            var validation = _validator.ValidateToCreate(expenseModel);
            if (!validation.Success)
            {
                return new ExpenseResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            var account = await _accountRepository.GetByIdAsync(expense.AccountId);

            account.Balance -= expense.Amount;

            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "A despesa não pode ser nula");
            }

            expenseModel.Date = DateTime.Now;

            await _expenseRepository.Create(expenseModel);
            
            return new ExpenseResponseDto 
            {
                Success = true,
                Data = expenseModel,
            };

        }

        public async Task<ExpenseResponseDto> DeleteExpenseAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id Inválido", nameof(id));
            }
            var getExpense = await _expenseRepository.GetById(id);
            if (getExpense == null)
            {
                throw new Exception($"A depesa com Id{id} não foi encontrada.");
            }
            var account = await _accountRepository.GetByIdAsync(getExpense.AccountId);
            account.Balance += getExpense.Amount;

            await _accountRepository.UpdateAsync(account);
            await _expenseRepository.Delete(getExpense);
            return new ExpenseResponseDto
            {
                Success = true,
                Message = $"A Despesa com Id:{id} Foi deletada com sucesso."
            };
        }

        public async Task<ImmutableList<ExpenseResponseDto>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAll();
            if (expenses == null || !expenses.Any())
            {
                throw new Exception("Nenhuma despesa foi encontrada.");
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
                throw new Exception($"Despesa com ID: {id} Não foi encontrada.");
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
                throw new Exception($"Despesa com nome: {name} Não foi encontrada.");
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
            var expenseModel = _mapper.Map<Expense>(expense);

            var existingExpense = await _expenseRepository.GetById(id);

            var validation = _validator.ValidateToUpdate(expenseModel);
            
            if (!validation.Success)
            {
                return new ExpenseResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            if (existingExpense == null)
            {
                throw new Exception($"Despesa com ID:{id} não foi encontrada.");
            }

            var diference = existingExpense.Amount - expense.Amount;

            var account = await _accountRepository.GetByIdAsync(expense.AccountId);

            account.Balance += diference;

            expenseModel.Id = id;

            await _expenseRepository.Update(expenseModel);
            await _accountRepository.UpdateAsync(account);

            return new ExpenseResponseDto 
            {
                Success = true,
                Data = expenseModel
            };
        }
    }
}

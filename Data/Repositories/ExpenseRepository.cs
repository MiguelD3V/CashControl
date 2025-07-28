using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Cashcontrol.API.Banco.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Expense expense)
        {
           _context.Expenses.Add(expense);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ImmutableList<Expense>> GetAll()
        {
            var expenses = _context.Expenses.ToImmutableList();
            await _context.SaveChangesAsync();
            return expenses;
        }

        public async Task<Expense> GetById(Guid id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(a => a.Id == id);
            if (expense == null)
            {
                throw new Exception($"A Despesa com {id} Não foi encontrada.");
            }
            return expense;
        }

        public async Task<Expense> GetByName(string name)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(a => a.Name == name);
            if (expense == null)
            {
                throw new Exception($"A Despesa com o nome {name} Não foi encontrada.");
            }
            return expense;
        }

        public async Task Update (Expense expense)
        {
            var Find = await _context.Expenses.FirstOrDefaultAsync(a => a.Id == expense.Id) ?? throw new Exception($"A Despesa com {expense.Id} Não foi encontrada.");

            expense.Id = Find.Id;
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
    }
}

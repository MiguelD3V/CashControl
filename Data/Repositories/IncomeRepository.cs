using Cashcontrol.API.Banco;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Cashcontrol.API.Data.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _context;

        public IncomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Income income)
        {
            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
        }

        public async Task<ImmutableList<Income>> GetAllAsync()
        {
            var incomes = _context.Incomes.ToImmutableList();
            await _context.SaveChangesAsync();
            return incomes;

        }

        public async Task<Income> GetByIdAsync(Guid id)
        {
            var income = await _context.Incomes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (income == null)
            {
                throw new Exception($"A Renda com o ID {id} não foi encontrada.");
            }
            return income;
        }

        public async Task UpdateAsync(Income income)
        {
            _context.Incomes.Update(income);
            await _context.SaveChangesAsync();
        }
    }
}

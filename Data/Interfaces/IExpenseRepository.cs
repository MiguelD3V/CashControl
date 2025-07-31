using Cashcontrol.API.Models.Bussines;
using System.Collections.Immutable;

namespace Cashcontrol.API.Banco.Interfaces
{
    public interface IExpenseRepository
    {
        public Task Create(Expense expense);
        public Task Update(Expense expense);
        public Task Delete(Expense expense);
        public Task<ImmutableList<Expense>> GetAll();
        public Task<Expense> GetById(Guid id);
        public Task<Expense> GetByName(string name);

    }
}

using Cashcontrol.API.Banco;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Cashcontrol.API.Data.Repositories
{
    public class AccountRepository(AppDbContext context) : IAccountRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Account?> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            var createdAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);
            return createdAccount;
        }

        public async Task DeleteAsync(Account account)
        {
            _context.Accounts.Remove(account);
             await _context.SaveChangesAsync();
        }

        public async Task<ImmutableList<Account>> GetAllAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return [.. accounts];
        }
        public async Task<Account> GetByIdAsync(Guid id)
        {
           var account =  await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
           return account ?? throw new Exception($"A Conta com o ID {id} não foi encontrada.");
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);
            return account ?? throw new Exception($"A Conta com o nome {name} não foi encontrada.");
        }


        public async Task<Account?> UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}

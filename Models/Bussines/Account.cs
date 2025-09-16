using System.Security.Principal;

namespace Cashcontrol.API.Models.Bussines
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}

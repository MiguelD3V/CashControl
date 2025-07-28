namespace Cashcontrol.API.Models.Bussines
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid AccountId { get; set; }
        public Account? Account { get; set; } 
        public ExpenseCategory Category { get; set; }
    }
}

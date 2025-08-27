using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos.Expense
{
    public class ExpenseResponseDto : ResponseBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid AccountId { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}

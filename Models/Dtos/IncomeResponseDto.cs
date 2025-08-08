using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class IncomeResponseDto : ResponseBase
    {
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public IncomeSource Source { get; set; }
        public bool IsRecurring { get; set; } = false;
        public int? RecurrenceId { get; set; } = null;
    }
}

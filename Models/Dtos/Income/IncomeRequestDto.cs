using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos.Income
{
    public class IncomeRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public IncomeSource Source { get; set; }
    }
}

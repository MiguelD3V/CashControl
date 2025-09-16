using Cashcontrol.API.Models.Bussines;
using System.Text.Json.Serialization;

namespace Cashcontrol.API.Models.Dtos.Expense
{
    public class ExpenseResponseDto : ResponseBase
    {
        [JsonIgnore]
        public string? Name { get; set; }
        [JsonIgnore]
        public string? Description { get; set; }
        [JsonIgnore]
        public decimal Amount { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        [JsonIgnore]
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public ExpenseCategory Category { get; set; }
    }
}

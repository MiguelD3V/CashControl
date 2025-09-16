using Cashcontrol.API.Models.Bussines;
using System.Text.Json.Serialization;

namespace Cashcontrol.API.Models.Dtos.Income
{
    public class IncomeResponseDto : ResponseBase
    {
        [JsonIgnore]
        public string? Description { get; set; }
        public string Name { get; set; } = String.Empty;
        [JsonIgnore]
        public decimal Amount { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        [JsonIgnore]
        public IncomeSource Source { get; set; }
        [JsonIgnore]
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public bool IsRecurring { get; set; } = false;
        [JsonIgnore]
        public int? RecurrenceId { get; set; } = null;
    }
}

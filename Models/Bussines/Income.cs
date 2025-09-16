using System.Text.Json.Serialization;

namespace Cashcontrol.API.Models.Bussines
{
    public class Income
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account? Account { get; set; }
        public IncomeSource Source { get; set; }
    }
}

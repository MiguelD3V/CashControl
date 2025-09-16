using Cashcontrol.API.Models.Bussines;
using System.Text.Json.Serialization;

namespace Cashcontrol.API.Models.Dtos.Account
{
    public class AccountResponseDto : ResponseBase
    {
        [JsonIgnore]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public decimal Balance { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public AccountType Type { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}

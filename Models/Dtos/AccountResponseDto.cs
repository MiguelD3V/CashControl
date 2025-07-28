using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class AccountResponseDto : ResponseBase
    {
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

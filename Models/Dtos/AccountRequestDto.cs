using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class AccountRequestDto : ResponseBase
    {
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
    }
}

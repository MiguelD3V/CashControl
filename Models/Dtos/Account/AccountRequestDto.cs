using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos.Account
{
    public class AccountRequestDto 
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public AccountType Type { get; set; }
    }
}

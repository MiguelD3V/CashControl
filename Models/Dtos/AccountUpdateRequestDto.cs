using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class AccountUpdateRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
    }
}


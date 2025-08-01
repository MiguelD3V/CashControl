﻿using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class AccountRequestDto 
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public AccountType Type { get; set; }
    }
}

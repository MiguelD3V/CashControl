﻿using Cashcontrol.API.Models.Bussines;

namespace Cashcontrol.API.Models.Dtos
{
    public class IncomeRequestDto
    {
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public IncomeSource Source { get; set; }
    }
}

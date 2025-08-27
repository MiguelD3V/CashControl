using Cashcontrol.API.Models.Bussines;
using System.ComponentModel.DataAnnotations;

namespace Cashcontrol.API.Models.Dtos.Expense
{
    public class ExpenseRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }

        [EnumDataType(typeof(ExpenseCategory), ErrorMessage = "Categoria inválida.")]
        public ExpenseCategory Category { get; set; }
    }
}

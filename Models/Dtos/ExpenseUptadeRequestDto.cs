using Cashcontrol.API.Models.Bussines;
using System.ComponentModel.DataAnnotations;

namespace Cashcontrol.API.Models.Dtos
{
    public class ExpenseUptadeRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }

        [EnumDataType(typeof(ExpenseCategory), ErrorMessage = "Categoria inválida.")]
        public ExpenseCategory Category { get; set; }
    }
}

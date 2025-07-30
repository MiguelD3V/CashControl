using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Workers;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseResponseDto>> Create(ExpenseRequestDto expense)
        {
            try
            {
                var createdExpense = await _expenseService.CreateExpenseAsync(expense);
                return createdExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }

        }

        [HttpPut("{id}")]
        public async  Task<ActionResult<ExpenseResponseDto>> Edit(Guid id, ExpenseRequestDto expense)
        {
            try
            { 
                var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expense);
                return updatedExpense;
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;
using Cashcontrol.API.Services.Workers;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("CashControl/api/[controller]")]
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
        public async Task<ActionResult<ExpenseResponseDto>> Edit(Guid id, ExpenseRequestDto expense)
        {
            try
            {
                var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expense);
                return updatedExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> Delete(Guid id)
        {
            try
            {
                var deletedExpense = await _expenseService.DeleteExpenseAsync(id);
                return deletedExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ImmutableList<ExpenseResponseDto>>> GetAll()
        {
            try
            {
                var expenses = await _expenseService.GetAllExpensesAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> GetById(Guid id)
        {
            try
            {
                var expense = await _expenseService.GetExpenseById(id);
                return expense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

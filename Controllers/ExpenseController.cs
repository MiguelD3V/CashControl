using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;
using Cashcontrol.API.Services.Workers;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<ExpenseResponseDto>> Create(ExpenseRequestDto expense)
        {

            try
            {
                var createdExpense = await _expenseService.CreateExpenseAsync(expense);
                List<string> errors = new List<string>();
                foreach (var error in createdExpense.Errors)
                {
                    errors.Add(error);
                }
                if (!createdExpense.Success)
                {
                    return BadRequest(errors);
                }
                return createdExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenseResponseDto>> Edit(Guid id, ExpenseRequestDto expense)
        {
            try
            {
                var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expense);
                List<string> errors = new List<string>();
                foreach (var error in updatedExpense.Errors)
                {
                    errors.Add(error);
                }
                if (!updatedExpense.Success)
                {
                    return BadRequest(errors);
                }
                return updatedExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenseResponseDto>> Delete(Guid id)
        {
            try
            {
                var deletedExpense = await _expenseService.DeleteExpenseAsync(id);
                List<string> errors = new List<string>();
                foreach (var error in deletedExpense.Errors)
                {
                    errors.Add(error);
                }
                if (!deletedExpense.Success)
                {
                    return BadRequest(errors);
                }
                return deletedExpense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ImmutableList<ExpenseResponseDto>>> GetAll()
        {
            try
            {
                var expenses = await _expenseService.GetAllExpensesAsync();
                if (expenses == null || !expenses.Any())
                {
                    return NotFound(new { message = "Nenhuma despesa encontrada." });
                }
                return expenses;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenseResponseDto>> GetById(Guid id)
        {
            try
            {
                var expense = await _expenseService.GetExpenseById(id);
                if (expense == null)
                {
                    return NotFound(new { message = "Despesa não encontrada." });
                }
                return expense;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

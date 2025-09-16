using Cashcontrol.API.Models.Dtos.Income;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("CashControl/api/[controller]")]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;
        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IncomeResponseDto>> Create(IncomeRequestDto income)
        {
            try
            {
                var createdIncome = await _incomeService.CreateAsync(income);
                List<string> errors = new List<string>();
                foreach (var error in createdIncome.Errors)
                {
                    errors.Add(error);
                }
                if (!createdIncome.Success)
                {
                    return BadRequest(errors);
                }
                return createdIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<IncomeResponseDto>> Edit(Guid id, IncomeRequestDto income)
        {
            try
            {
                var updatedIncome = await _incomeService.UpdateAsync(income, id);
                List<string> errors = new List<string>();
                foreach (var error in updatedIncome.Errors)
                {
                    errors.Add(error);
                }
                if (!updatedIncome.Success)
                {
                    return BadRequest(errors);
                }
                return updatedIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<IncomeResponseDto>> Delete(Guid id)
        {
            try
            {
                var deletedIncome = await _incomeService.DeleteAsync(id);
                List<string> errors = new List<string>();
                foreach (var error in deletedIncome.Errors)
                {
                    errors.Add(error);
                }
                if (!deletedIncome.Success)
                {
                    return BadRequest(errors);
                }
                return deletedIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IncomeResponseDto>> GetById(Guid id)
        {
            try
            {
                var income = await _incomeService.GetByIdAsync(id);
                if (income == null)
                {
                    return NotFound(new { message = "Despesa não encontrada"});
                }
                return income;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IImmutableList<IncomeResponseDto>>> GetAll()
        {
            try
            {
                var incomes = await _incomeService.GetAllAsync();
                if (incomes == null)
                {
                    return NotFound(new { message = "Nenhuma receita encontrada" });
                }
                return Ok(incomes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

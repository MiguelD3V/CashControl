using Cashcontrol.API.Models.Dtos.Income;
using Cashcontrol.API.Services.Workers.Interfaces;
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
        public async Task<ActionResult<IncomeResponseDto>> Create(IncomeRequestDto income)
        {
            try
            {
                var createdIncome = await _incomeService.CreateAsync(income);
                return createdIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<IncomeResponseDto>> Edit(Guid id, IncomeRequestDto income)
        {
            try
            {
                var updatedIncome = await _incomeService.UpdateAsync(income, id);
                return updatedIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncomeResponseDto>> Delete(Guid id)
        {
            try
            {
                var deletedIncome = await _incomeService.DeleteAsync(id);
                return deletedIncome;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
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
        public async Task<ActionResult<IImmutableList<IncomeResponseDto>>> GetAll()
        {
            try
            {
                var incomes = await _incomeService.GetAllAsync();
                return Ok(incomes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

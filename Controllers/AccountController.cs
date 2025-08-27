using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Account;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("CashControl/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponseDto>> CreateAccountAsync([FromBody] AccountRequestDto accountRequest)
        {
            try
            {
                var createdAccount = await _accountService.CreateAsync(accountRequest);
                return createdAccount;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<AccountResponseDto>> UpdateAccountAsync(string email, [FromBody]AccountUpdateRequestDto account)
        {
            try
            {
                var updatedAccount = await _accountService.UpdateAsync(email, account);
                return updatedAccount;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponseDto>> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var account = await _accountService.GetByIdAsync(id);
                if (account == null)
                {
                    return NotFound(new { message = "Conta não encontrada" });
                }
                return account;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult<IImmutableList<AccountResponseDto>>> GetAllAccountsAsync()
        {
            try
            {
                var accounts = await _accountService.GetAllAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete]
        public async Task<ActionResult<AccountResponseDto>> DeleteAccountAsync(Guid id)
        {
            try
            {
                var deletedAccount = await _accountService.DeleteAsync(id);
                if (deletedAccount == null)
                {
                    return NotFound(new { message = "Conta não encontrada" });
                }
                return deletedAccount;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

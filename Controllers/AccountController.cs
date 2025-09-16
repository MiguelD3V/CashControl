using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Account;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<AccountResponseDto>> CreateAccountAsync([FromBody] AccountRequestDto accountRequest)
        {
            try
            {
                var createdAccount = await _accountService.CreateAsync(accountRequest);
                List<string> errors = new List<string>();
                foreach (var error in createdAccount.Errors)
                {
                    errors.Add(error);
                }
                if (!createdAccount.Success)
                {
                   return BadRequest(errors);
                }

                return createdAccount;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<AccountResponseDto>> UpdateAccountAsync(Guid id, [FromBody]AccountUpdateRequestDto account)
        {
            try
            {
                var updatedAccount = await _accountService.UpdateAsync(id, account);
                List<string> errors = new List<string>();
                foreach (var error in updatedAccount.Errors)
                {
                    errors.Add(error);
                }
                if (!updatedAccount.Success) 
                {
                    return BadRequest(errors);
                }
                return updatedAccount;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AccountResponseDto>> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var account = await _accountService.GetByIdAsync(id);
                if (!account.Success)
                {
                    return NotFound(new { message = "Nenhuma conta encontrada" });
                }
                return account;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IImmutableList<AccountResponseDto>>> GetAllAccountsAsync()
        {
            try
            {
                var accounts = await _accountService.GetAllAsync();
                if (accounts == null || accounts.Count == 0)
                {
                    return NotFound(new { message = "Nenhuma conta encontrada" });
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<AccountResponseDto>> DeleteAccountAsync(Guid id)
        {
            try
            {
                var deletedAccount = await _accountService.DeleteAsync(id);
                List<string> errors = new List<string>();
                foreach (var error in deletedAccount.Errors)
                {
                    errors.Add(error);
                }
                if (!deletedAccount.Success)
                {
                    return BadRequest(errors);
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

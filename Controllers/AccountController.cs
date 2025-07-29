using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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


    }
}
